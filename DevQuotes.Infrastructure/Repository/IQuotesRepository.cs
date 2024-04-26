using DevQuotes.Domain.Entities;
using DevQuotes.Infrastructure.Helpers.Pagination;
using DevQuotes.Shared;
using System.Linq.Expressions;

namespace DevQuotes.Infrastructure.Repository
{
    public interface IQuotesRepository
    {
        Task<Result> AddAsync(Quote quote, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedList<Quote>> GetAllAsync(Parameters parameters, Expression<Func<Quote, bool>>? expression = null, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default);
        Task<Quote?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<HashSet<string>> GetLanguagesAsync(CancellationToken cancellationToken = default);
        Task<Quote?> GetRandomQuoteAsync(CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Quote quote, CancellationToken cancellationToken = default);
    }
}