using DevQuotes.Domain.Entities;
using DevQuotes.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DevQuotes.Infrastructure.Helpers.Pagination;

namespace DevQuotes.Infrastructure.Repository;

public class QuotesRepository(ApplicationDbContext dbContext) : IQuotesRepository
{
    private readonly Random _random = new();
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Quote?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Quotes.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);

    public async Task<Quote?> GetRandomQuoteAsync(CancellationToken cancellationToken = default)
    {
        var quotes = _dbContext.Quotes.OrderByDescending(x => x.Created).AsNoTracking();

        if (FakeCache.CanRefreshCache)
        {
            FakeCache.Count = await quotes.CountAsync(cancellationToken);
            FakeCache.LastUpdate = DateTime.UtcNow;
        }

        return await quotes.Skip(_random.Next(FakeCache.Count)).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedList<Quote>> GetAllAsync(Parameters parameters, Expression<Func<Quote, bool>>? expression = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Quotes.OrderByDescending(x => x.Created).AsNoTracking();

        if (expression is not null)
        {
            query = query.Where(expression);
        }

        if (ignoreQueryFilter)
        {
            query = query.IgnoreQueryFilters();
        }

        return await PagedList<Quote>.ToPagedList(query, parameters.Page, parameters.Limit, cancellationToken);
    }

    public async Task<Result> AddAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        await _dbContext.Quotes.AddAsync(quote, cancellationToken);
        return await _dbContext.SaveAsync(cancellationToken: cancellationToken);
    }

    public async Task<Result> UpdateAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        _dbContext.Quotes.Update(quote);
        return await _dbContext.SaveAsync(cancellationToken: cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var quote = await GetAsync(id, cancellationToken);

        if (quote is null)
        {
            return Result.Fail(nameof(id), "Quote not found.");
        }

        quote.Delete();
        await UpdateAsync(quote, cancellationToken);
        return Result.Success();
    }

    public async Task<HashSet<string>> GetLanguagesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Quotes.AsNoTracking()
                .OrderBy(x => x.Language)
                .Where(x => !string.IsNullOrWhiteSpace(x.Language))
                .Select(x => x.Language.ToLower())
                .ToListAsync(cancellationToken);

        return result.Distinct().ToHashSet();
    }
}

public static class FakeCache
{
    public static int Count { get; set; }
    public static DateTime LastUpdate { get; set; }

    public static bool CanRefreshCache => Count == 0 || DateTime.UtcNow.Subtract(LastUpdate).TotalMinutes > 10;
}