namespace WebNoteMiniApp.Data
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteDb db;
        private readonly UserService userService;

        public NoteRepository(NoteDb db, UserService userService)
        {
            this.db = db;
            this.userService = userService;
        }


        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            var userId = await userService.GetUserId();
            return await db.Notes.Where(n => n.OwnerId == userId).ToArrayAsync();
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            var userId = await userService.GetUserId();
            return await db.Notes.FirstOrDefaultAsync(n => n.Id == id && n.OwnerId == userId);
        }

        public async Task<IEnumerable<Note>> GetByTitleAsync(string title)
        {
            var userId = await userService.GetUserId();
            return await db.Notes
                .Where(n => n.Title.Contains(title) && n.OwnerId == userId)
                .ToArrayAsync();
        }

        public async Task AddAsync(Note note)
        {
            var userId = await userService.GetUserId();
            note.OwnerId = userId;
            note.Created = DateTime.Now.ToString();
            await db.Notes.AddAsync(note);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Note note)
        {
            var res = await db.Notes.FirstOrDefaultAsync(n => n.Id == note.Id);
            if (res is null) return;
            res.Title = note.Title;
            res.Text = note.Text;
            await db.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var res = await db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (res is null) return;
            db.Remove(res);
            await db.SaveChangesAsync();
        }


    }
}
