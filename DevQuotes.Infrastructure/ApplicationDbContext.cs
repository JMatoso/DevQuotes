using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;
using DevQuotes.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DevQuotes.Infrastructure;

public class ApplicationDbContext : DbContext
{
    private readonly ILogger<ApplicationDbContext> _logger;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger) 
        : base(options)
    {
        _logger = logger;
        Database.EnsureCreated();
        SeedQuotes();
    }

    public DbSet<Quote> Quotes => Set<Quote>();
    public DbSet<Language> Languages => Set<Language>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasIndex(e => e.Content);
            entity.HasQueryFilter(e => !e.IsDeleted);
            entity.HasOne(e => e.Language)
                .WithMany(e => e.Quotes)
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasIndex(e => e.Name);
            entity.HasIndex(e => e.Code);
            entity.HasIndex(e => new { e.Name, e.Code }).IsUnique();
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        base.OnModelCreating(modelBuilder);
    }

    private void SeedQuotes()
    {
        if (Quotes.Any())
        {
            return;
        }

        // fix seed file path

        var seedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "quotes.v2.json");

        if (File.Exists(seedFilePath))
        {
            var fileContent = File.ReadAllText(seedFilePath);
            var quotesToSeed = JsonConvert.DeserializeObject<List<LanguageResponse>>(fileContent) ?? [];

            quotesToSeed.ForEach((item) =>
            {
                var language = new Language()
                {
                    Name = item.Name,
                    Code = item.Code
                };

                language.Quotes = item.Quotes.Select(q => new Quote()
                {
                    Content = q.Content,
                    Language = language
                }).ToList();

                Languages.Add(language);
            });

            SaveChanges();
        }
    }

    public async Task<Result> SaveAsync(bool throwsConcurrencyException = true, CancellationToken cancellationToken = default)
    {
        try
        {
            return await SaveChangesAsync(cancellationToken) > 0 
                ? Result.Success() 
                : Result.Fail("No changes were made.");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (throwsConcurrencyException)
            {
                throw;
            }

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
