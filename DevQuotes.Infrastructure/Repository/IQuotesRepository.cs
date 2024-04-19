using DevQuotes.Domain.Entities;
using DevQuotes.Infrastructure.Helpers.Pagination;
using DevQuotes.Shared;
using System.Linq.Expressions;

namespace DevQuotes.Infrastructure.Repository
{
    public interface IQuotesRepository
    {
        Task AddAsync(Quote quote);
        Task<Result> DeleteAsync(Guid id);
        Task<PagedList<Quote>> GetAllAsync(Parameters parameters, Expression<Func<Quote, bool>>? expression = null, bool ignoreQueryFilter = false);
        Task<Quote?> GetAsync(Guid id);
        Task<IEnumerable<string>> GetLanguagesAsync();
        Task<Quote?> GetRandomQuoteAsync();
        Task UpdateAsync(Quote quote);
    }
}