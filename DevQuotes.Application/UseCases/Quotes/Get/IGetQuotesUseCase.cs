using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;
using DevQuotes.Infrastructure.Helpers.Pagination;
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Quotes.Get
{
    public interface IGetQuotesUseCase
    {
        Task<Result<QuoteResponse>> ExecuteAsync(string code = "", CancellationToken cancellationToken = default);
        Task<Quote?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedQuoteResponse> ExecuteAsync(PaginationParameters parameters, string keyword = "", CancellationToken cancellationToken = default);
    }
}