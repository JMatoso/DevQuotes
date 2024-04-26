using DevQuotes.Communication.Requests;
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Quotes.Update
{
    public interface IUpdateQuoteUseCase
    {
        Task<Result<bool>> ExecuteAsync(Guid id, QuoteJsonRequest quote, CancellationToken cancellationToken = default);
    }
}