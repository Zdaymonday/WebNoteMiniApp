using Azure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebNoteMiniApp.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NoteDb>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("note_data"));
});
builder.Services.AddTransient<INoteRepository, NoteRepository>();
builder.Services.AddSingleton<IUserRepository> (new UserRepository());
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

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/login", [AllowAnonymous] (IUserRepository userRep, ITokenService tokenServ, [FromBody]UserModel model, HttpContext context) =>
{
    var dto = userRep.GetUser(model);
    if (dto == null) return Results.Unauthorized();
    var key = builder.Configuration["Jwt:Key"];
    var issue = builder.Configuration["Jwt:Issuer"];

    var token = tokenServ.BuildToken(key, issue, dto);

    return Results.Ok(token);
});


app.MapGet("/notes", async (INoteRepository rep) => await rep.GetAllAsync())
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
