using DevQuotes.Communication.Responses;
using DevQuotes.Infrastructure.Helpers.Pagination;
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Quotes.Get
{
    public interface IGetQuotesUseCase
    {
        Task<Result<QuoteResponse>> ExecuteAsync(CancellationToken cancellationToken = default);
        Task<Result<QuoteResponse>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedQuoteResponse> ExecuteAsync(PaginationParameters parameters, string keyword = "", CancellationToken cancellationToken = default);
    }
}