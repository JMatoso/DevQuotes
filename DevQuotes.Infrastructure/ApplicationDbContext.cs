using DevQuotes.Communication.Requests;
using DevQuotes.Domain.Entities;
using DevQuotes.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DevQuotes.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Quote> Quotes => Set<Quote>();

        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger) 
            : base(options)
        {
            _logger = logger;
            Database.EnsureCreated();
            SeedQuotes();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>(entity =>
            {
                entity.HasQueryFilter(e => !e.IsDeleted);
                entity.HasIndex(e => new { e.Content, e.Language });
            });

            base.OnModelCreating(modelBuilder);
        }

        private void SeedQuotes()
        {
            if (Quotes.Any()) return;

            var seedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "quotes.v2.json");

            if (File.Exists(seedFilePath))
            {
                string fileContent = File.ReadAllText(seedFilePath);
                var oldQuotes = JsonConvert.DeserializeObject<List<QuoteJsonRequest>>(fileContent) ?? [];

                oldQuotes.ForEach((item) =>
                {
                    Quotes.Add(new Quote()
                    {
                        Content = item.Content,
                        Language = item.Language
                    });
                });

                SaveChanges();
            }
        }

        public async Task<Result> SaveAsync(bool throwsConcurrencyException = true)
        {
            try
            {
                return await SaveChangesAsync() > 0 ?
                            Result.Success() :
                            Result.Fail("No changes were made.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (throwsConcurrencyException)
                    throw;
                return Result.Fail(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error saving AppDbContext.SaveAsync()");
#if DEBUG
                return Result.Fail(ex.ToString());
#else
                return Result.Fail(ex.Message);
#endif
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving AppDbContext.SaveAsync()");

#if DEBUG
                return Result.Fail(ex.ToString());
#else
                return Result.Fail(ex.Message);
#endif
            }
        }
    }
}
