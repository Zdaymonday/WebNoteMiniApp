using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WebNoteMiniApp.Data
{
    public class NoteDb : DbContext
    {
        public NoteDb(DbContextOptions<NoteDb> opt) : base(opt) { }
        public DbSet<Note> Notes { get; set; } = null!;

        
        //public NoteDb() { }
        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    builder.UseSqlServer("Server = .\\sqlexpress; Database = note_data; Trusted_Connection = true; TrustServerCertificate = true;");
        //}
    }
}
