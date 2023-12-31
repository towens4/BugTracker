﻿using Microsoft.EntityFrameworkCore;

namespace BugTrackerCore.Models
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
