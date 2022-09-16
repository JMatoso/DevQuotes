using DevQuotes.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevQuotes.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Quote> Quotes => Set<Quote>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
