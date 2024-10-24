using DevQuotes.Communication.Requests;
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Quotes.Update
{
    public interface IUpdateQuoteUseCase
    {
        Task<Result<bool>> ExecuteAsync(Guid id, QuoteRequest quote, CancellationToken cancellationToken = default);
    }
}