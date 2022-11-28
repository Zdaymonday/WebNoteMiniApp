var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NoteDb>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("note_data"));
});
builder.Services.AddTransient<INoteRepository, NoteRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/notes", async (INoteRepository rep) => await rep.GetAll());
app.MapGet("/notes/{id}", async (INoteRepository rep, int id) =>
{
    var user = await rep.GetById(id);
    if (user == null) return Results.NotFound();
    return Results.Ok(user);
});

app.MapPost("/notes", async ([FromBody] Note note, [FromServices] INoteRepository rep) =>
{
    await rep.Add(note);
    return Results.Created($"/notes/{note.Id}", note);
});

app.MapPut("/notes", async (INoteRepository rep, [FromBody] Note note) =>
{
    await rep.Update(note);
    return Results.Ok();
});
app.MapDelete("/notes/{id}", async (INoteRepository rep,  int id) =>
{
    await rep.Remove(id);
    return Results.Ok();
});

app.UseHttpsRedirection();

app.Run();
