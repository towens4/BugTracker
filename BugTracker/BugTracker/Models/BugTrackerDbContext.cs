using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    public class BugTrackerDbContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Error> Errors { get; set; }

        public BugTrackerDbContext(DbContextOptions<BugTrackerDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
