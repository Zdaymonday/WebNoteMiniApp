namespace WebNoteMiniApp.Data
{
    public class NoteDb : DbContext
    {
        public NoteDb(DbContextOptions<NoteDb> opt) : base(opt) { }
        public DbSet<Note> Notes { get; set; } = null!;
    }
}
