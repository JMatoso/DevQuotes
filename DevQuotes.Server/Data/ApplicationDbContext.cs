using DevQuotes.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DevQuotes.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Quote> Quotes => Set<Quote>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>().ToTable("Quotes");
            modelBuilder.Entity<Quote>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Content).IsUnique();
                entity.Property(e => e.Created).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
