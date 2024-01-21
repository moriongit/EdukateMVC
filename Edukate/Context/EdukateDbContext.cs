using Edukate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Edukate.Context
{
    public class EdukateDbContext : IdentityDbContext
    {
        public EdukateDbContext(DbContextOptions<EdukateDbContext>options) : base(options)
        {
        }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<AppUser> Appusers { get; set; }
    }
}
