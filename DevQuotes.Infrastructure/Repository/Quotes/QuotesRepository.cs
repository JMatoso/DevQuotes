using DevQuotes.Domain.Entities;
using DevQuotes.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DevQuotes.Infrastructure.Helpers.Pagination;
using DevQuotes.Communication.Responses;

namespace DevQuotes.Infrastructure.Repository.Quotes;

public sealed class QuotesRepository(ApplicationDbContext dbContext) : IQuotesRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async ValueTask<Quote?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Quotes.FindAsync([id], cancellationToken);
    }

    public async Task<QuoteResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Quotes.Select(x => new QuoteResponse()
        {
            Id = x.Id,
            Content = x.Content,
            CreatedAt = x.CreatedAt,
            Language = new LanguageResponse()
            {
                Id = x.Language.Id,
                Name = x.Language.Name,
                Code = x.Language.Code,
            }
        }).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<QuoteResponse?> GetRandomAsync(CancellationToken cancellationToken = default)
    {
        var quotes = _dbContext.Quotes.AsNoTracking().OrderByDescending(x => x.CreatedAt);

        // todo: remove this block
        //if (FakeCache.CanRefreshCache)
        //{
        //    FakeCache.Count = await quotes.CountAsync(cancellationToken);
        //    FakeCache.LastUpdate = DateTime.UtcNow;
        //}

        return await quotes
            .OrderBy(x => EF.Functions.Random())
            .Select(x => new QuoteResponse()
            {
                Id = x.Id,
                Content = x.Content,
                Language = new LanguageResponse()
                {
                    Id = x.Language.Id,
                    Name = x.Language.Name,
                    Code = x.Language.Code,
                }
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedList<QuoteResponse>> GetAllAsync(PaginationParameters parameters, Expression<Func<Quote, bool>>? expression = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Quotes.AsNoTracking().OrderByDescending(x => x.CreatedAt).AsQueryable();

        if (expression is not null)
        {
            query = query.Where(expression);
        }

        if (ignoreQueryFilter)
        {
            query = query.IgnoreQueryFilters();
        }

        var finalQuery = query.Select(x => new QuoteResponse()
        {
            Id = x.Id,
            Content = x.Content,
            Language = new LanguageResponse()
            {
                Id = x.Language.Id,
                Name = x.Language.Name,
                Code = x.Language.Code,
            }
        });

        return await PagedList<QuoteResponse>.ToPagedList(finalQuery, parameters.Page, parameters.Limit, cancellationToken);
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
        var result = await _dbContext.Quotes.Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(y => y.IsDeleted, true), cancellationToken);
        return result > 0 ? Result.Success() : Result.Fail("No changes were made.");
    }
}

public static class FakeCache
{
    public static int Count { get; set; }
    public static DateTime LastUpdate { get; set; }

    public static bool CanRefreshCache => Count == 0 || DateTime.UtcNow.Subtract(LastUpdate).TotalMinutes > 10;
}