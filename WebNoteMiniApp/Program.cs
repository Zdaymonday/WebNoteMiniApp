using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NoteDb>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("note_data"));
});
builder.Services.AddDbContext<UserDb>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("note_users"));
});

builder.Services.AddIdentity<NoteUser, IdentityRole>().AddEntityFrameworkStores<UserDb>();

builder.Services.AddTransient<INoteRepository, NoteRepository>();
builder.Services.AddSingleton<ITokenService>(new TokenService());

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        };
    });

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", opt =>
    {
        opt.AllowAnyHeader();
        opt.AllowAnyMethod();
        opt.AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();



if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/login", [AllowAnonymous] async (SignInManager<NoteUser> signInManager, ITokenService tokenServ, [FromBody] UserModel model) =>
{
    var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
    if (!result.Succeeded) return Results.Unauthorized();
    var key = builder.Configuration["Jwt:Key"];
    var issue = builder.Configuration["Jwt:Issuer"];

    var token = tokenServ.BuildToken(key, issue, new(model.UserName, model.Password));

    return Results.Ok(token);
});

app.MapPost("/register", [AllowAnonymous] async (UserManager<NoteUser> userManager, ITokenService tokenServ, [FromBody] UserModel model, HttpContext context) =>
{
    var user = new NoteUser() { UserName = model.UserName };
    var result = await userManager.CreateAsync(user, model.Password);

    if (!result.Succeeded) return Results.BadRequest(result.Errors);

    return Results.LocalRedirect("/login", preserveMethod: true);
});


app.MapGet("/notes", [Authorize] async (INoteRepository rep) => await rep.GetAllAsync())
    .Produces<List<Note>>(StatusCodes.Status200OK)
    .WithName("GetAllNotes")
    .WithTags("Getters");


app.MapGet("/notes/{id:int}", async (INoteRepository rep, int id) =>
    {
        var note = await rep.GetByIdAsync(id);
        if (note == null) return Results.NotFound();
        return Results.Ok(note);
    })
    .Produces<Note>(StatusCodes.Status200OK)
    .WithName("Get Single Note by Id")
    .WithTags("Getters");

app.MapGet("/notes/{title}", async (INoteRepository rep, string title) =>
{
    var notes = await rep.GetByTitleAsync(title);
    if (notes == null || notes.Any()) return Results.NotFound();
    return Results.Ok(notes);
})
    .Produces<Note>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("Get notes which contain the search patten in the title")
    .WithTags("Getters");

app.MapPost("/notes", async ([FromBody] Note note, [FromServices] INoteRepository rep) =>
    {
        await rep.AddAsync(note);
        return Results.Created($"/notes/{note.Id}", note);
    })
    .Accepts<Note>("application/json")
    .WithName("Create single note")
    .WithTags("Creators");

app.MapPut("/notes", async (INoteRepository rep, [FromBody] Note note) =>
    {
        await rep.UpdateAsync(note);
        return Results.Ok();
    })
    .Accepts<Note>("application/json")
    .WithName("Edit note")
    .WithTags("Updaters");

app.MapDelete("/notes/{id}", async (INoteRepository rep, int id) =>
    {
        await rep.RemoveAsync(id);
        return Results.Ok();
    })
    .WithName("Remove single note by id")
    .WithTags("Remove");

app.UseHttpsRedirection();

app.Run();
