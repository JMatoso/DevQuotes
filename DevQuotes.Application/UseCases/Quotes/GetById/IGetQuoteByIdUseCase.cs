using DevQuotes.Communication.Responses;
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Quotes.GetById
{
    public interface IGetQuoteByIdUseCase
    {
        Task<Result<QuoteJsonResponse>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}