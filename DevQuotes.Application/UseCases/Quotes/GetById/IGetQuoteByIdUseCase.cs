using DevQuotes.Communication.Responses;

namespace DevQuotes.Application.UseCases.Quotes.GetById
{
    public interface IGetQuoteByIdUseCase
    {
        Task<QuoteJsonResponse> ExecuteAsync(Guid id);
    }
}