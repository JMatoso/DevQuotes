using DevQuotes.Communication.Responses;

namespace DevQuotes.Application.UseCases.Quotes.GetRandom
{
    public interface IGetRandomQuoteUseCase
    {
        Task<QuoteJsonResponse> ExecuteAsync();
    }
}