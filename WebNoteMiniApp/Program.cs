using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NoteDb>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("note_data"));
});

var app = builder.Build();

app.MapGet("/notes", async (NoteDb db) => await db.Notes.ToArrayAsync());
app.MapGet("/notes/{id}", async (NoteDb db, int id) =>
{
    var user = await db.Notes.FirstOrDefaultAsync(n => n.Id == id);
    if (user == null) return Results.NotFound();
    return Results.Ok(user);
});

app.MapPost("/notes", async ([FromBody] Note note, [FromServices] NoteDb db) =>
{
    await db.Notes.AddAsync(note);
    var res = await db.SaveChangesAsync();
    if (res != 0) return Results.Created($"/notes/{note.Id}", note);
    return Results.BadRequest();
});

app.MapPut("/notes", async (NoteDb db, [FromBody] Note note) =>
{
    var isExist = await db.Notes.ContainsAsync(note);
    if (isExist) db.Notes.Update(note);
    else return Results.NotFound();
    await db.SaveChangesAsync();
    return Results.Ok();
});
app.MapDelete("/notes/{id}", async (NoteDb db,  int id) =>
{
    var note = await db.Notes.FirstOrDefaultAsync(n => n.Id == id);

    if (note is null)  Results.NotFound();

    db.Notes.Remove(note);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.UseHttpsRedirection();

app.Run();
