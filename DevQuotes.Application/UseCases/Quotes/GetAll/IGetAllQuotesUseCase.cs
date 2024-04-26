using DevQuotes.Communication.Responses;
using DevQuotes.Infrastructure.Helpers.Pagination;

namespace DevQuotes.Application.UseCases.Quotes.GetAll
{
    public interface IGetAllQuotesUseCase
    {
        Task<ListQuoteJsonResponse> ExecuteAsync(Parameters parameters, string keyword = "", CancellationToken cancellationToken = default);
    }
}