using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebNoteMiniApp.Data
{
    public class UserDb : IdentityDbContext
    {
        public UserDb(DbContextOptions<UserDb> opt) : base(opt) { }

        public DbSet<NoteUser> NoteUsers { get; set; } = null!;
    }
}
