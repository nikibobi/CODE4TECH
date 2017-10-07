using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CODE4TECH.Database
{
    public class Code4TechDbContext : DbContext
    {
        public DbSet<Reading> Readings { get; set; }

        public Code4TechDbContext(DbContextOptions<Code4TechDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Reading>(reading =>
            {
                reading.HasKey(r => new { r.DeviceId, r.Time });

                reading.Property(r => r.Time)
                    .HasDefaultValueSql("GETUTCDATE()");
            });

            base.OnModelCreating(builder);
        }
    }
}
