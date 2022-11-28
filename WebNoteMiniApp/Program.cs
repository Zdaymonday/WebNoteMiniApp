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
