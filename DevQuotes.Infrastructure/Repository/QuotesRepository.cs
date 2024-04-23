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

    public async Task<Quote?> GetAsync(Guid id) => await _dbContext.Quotes.FindAsync(id);

    public async Task<Quote?> GetRandomQuoteAsync()
    {
        var quotes = _dbContext.Quotes.OrderByDescending(x => x.Created).AsNoTracking();

        if (FakeCache.CanRefreshCache)
        {
            FakeCache.Count = await quotes.CountAsync();
            FakeCache.LastUpdate = DateTime.UtcNow;
        }

        return await quotes.Skip(_random.Next(FakeCache.Count)).FirstOrDefaultAsync();
    }

    public async Task<PagedList<Quote>> GetAllAsync(Parameters parameters, Expression<Func<Quote, bool>>? expression = null, bool ignoreQueryFilter = false)
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

        return await PagedList<Quote>.ToPagedList(query, parameters.Page, parameters.Limit);
    }

    public async Task AddAsync(Quote quote)
    {
        await _dbContext.Quotes.AddAsync(quote);
        await _dbContext.SaveAsync();
    }

    public async Task UpdateAsync(Quote quote)
    {
        _dbContext.Quotes.Update(quote);
        await _dbContext.SaveAsync();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var quote = await GetAsync(id);

        if (quote is null)
            return Result.Fail("Quote not found");

        quote.Delete();
        await UpdateAsync(quote);
        return Result.Success();
    }

    public async Task<IEnumerable<string>> GetLanguagesAsync()
    {
        var result = await _dbContext.Quotes.AsNoTracking()
                .OrderBy(x => x.Language)
                .Where(x => !string.IsNullOrWhiteSpace(x.Language))
                .Select(x => x.Language.ToLower())
                .ToListAsync();

        return result.Distinct();
    }
}

public static class FakeCache
{
    public static int Count { get; set; }
    public static DateTime LastUpdate { get; set; }

    public static bool CanRefreshCache => Count == 0 || DateTime.UtcNow.Subtract(LastUpdate).TotalMinutes > 10;
}