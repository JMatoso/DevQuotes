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

    public async Task<QuoteResponse?> GetRandomAsync(string code = "", CancellationToken cancellationToken = default)
    {
        var quotes = _dbContext.Quotes.AsNoTracking().OrderByDescending(x => x.CreatedAt).AsQueryable();

        if (!string.IsNullOrWhiteSpace(code))
        {
            quotes = quotes.Where(x => x.Language.Code.Equals(code));
        }

        return await quotes
            .OrderBy(x => EF.Functions.Random())
            .Select(x => new QuoteResponse()
            {
                Content = x.Content,
                Language = new LanguageResponse()
                {
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
            Content = x.Content,
            Language = new LanguageResponse()
            {
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