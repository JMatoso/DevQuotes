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

            InitQuotes();
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

        private void InitQuotes()
        {
            if (Quotes.Any()) return;

            string fileContent = File.ReadAllText("Data/quotes.json");
            var oldQuotes = JsonConvert.DeserializeObject<List<Quote>>(fileContent);

            var quotes = new List<Quote>();

            oldQuotes!.ForEach((item) =>
            {
                quotes.Add(new Quote()
                {
                    Content = item.Content
                });
            });

            if(quotes != null)
            {
                Quotes.AddRange(quotes);
                SaveChanges();

                File.WriteAllText("Data/quotes.v2.json", JsonConvert.SerializeObject(quotes, Formatting.Indented));
            }
        }
    }
}
