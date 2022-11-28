namespace WebNoteMiniApp.Data
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteDb db;

        public NoteRepository(NoteDb db)
        {
            this.db = db;
        }


        public async Task<IEnumerable<Note>> GetAll()
        {
            return await db.Notes.ToArrayAsync();
        }

        public async Task<Note> GetById(int id)
        {
            return await db.Notes.FirstOrDefaultAsync(note => note.Id == id);
        }

        public async Task Add(Note note)
        {
            await db.Notes.AddAsync(note);
            await db.SaveChangesAsync();
        }

        public async Task Update(Note note)
        {
            var res = await db.Notes.FirstOrDefaultAsync(n => n.Id == note.Id);
            if (res is null) return;
            res.Title = note.Title;
            res.Text = note.Text;
            await db.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var res = await db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (res is null) return;
            db.Remove(res);
            await db.SaveChangesAsync();
        }  
    }
}
