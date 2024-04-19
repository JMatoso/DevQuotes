using DevQuotes.Communication.Requests;

namespace DevQuotes.Application.UseCases.Quotes.Update
{
    public interface IUpdateQuoteUseCase
    {
        Task ExecuteAsync(Guid id, QuoteJsonRequest quote);
    }
}