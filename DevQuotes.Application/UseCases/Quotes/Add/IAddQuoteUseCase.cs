using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;

namespace DevQuotes.Application.UseCases.Quotes.Add
{
    public interface IAddQuoteUseCase
    {
        Task<QuoteJsonResponse> ExecuteAsync(QuoteJsonRequest newQuote);
    }
}