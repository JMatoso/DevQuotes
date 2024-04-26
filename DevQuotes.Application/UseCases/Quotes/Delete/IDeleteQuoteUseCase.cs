
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Quotes.Delete
{
    public interface IDeleteQuoteUseCase
    {
        Task<Result<bool>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}