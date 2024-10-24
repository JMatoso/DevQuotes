using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Quotes.Add
{
    public interface IAddQuoteUseCase
    {
        Task<Result<QuoteResponse>> ExecuteAsync(QuoteRequest newQuote, CancellationToken cancellationToken = default);
    }
}