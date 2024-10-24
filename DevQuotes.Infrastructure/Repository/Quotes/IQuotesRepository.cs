using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;
using DevQuotes.Infrastructure.Helpers.Pagination;
using DevQuotes.Shared;
using System.Linq.Expressions;

namespace DevQuotes.Infrastructure.Repository.Quotes
{
    public interface IQuotesRepository
    {
        Task<Result> AddAsync(Quote quote, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        ValueTask<Quote?> FindAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedList<QuoteResponse>> GetAllAsync(PaginationParameters parameters, Expression<Func<Quote, bool>>? expression = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default);
        Task<QuoteResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<QuoteResponse?> GetRandomAsync(CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Quote quote, CancellationToken cancellationToken = default);
    }
}