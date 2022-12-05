using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebNoteMiniApp.Identity.Models;

namespace WebNoteMiniApp.Identity.Context
{
    public class UserDb : IdentityDbContext
    {
        public UserDb(DbContextOptions<UserDb> opt) : base(opt) { }

        DbSet<NoteUser> NoteUsers { get; set; } = null!;

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .\\sqlexpress; Database = note_users; Trusted_Connection = true; TrustServerCertificate = true;");
        }
        */
    }
}
