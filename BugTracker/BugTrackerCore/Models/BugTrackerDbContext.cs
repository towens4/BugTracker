using Microsoft.EntityFrameworkCore;
using BugTrackerAPICall.Models;
using BugTrackerAPICall.Interfaces;

namespace BugTrackerAPICall.Models
{
    public class BugTrackerDbContext : DbContext
    {
        public DbSet<Application> Application { get; set; }
        public DbSet<Error> Error { get; set; }

        public BugTrackerDbContext(DbContextOptions<BugTrackerDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
