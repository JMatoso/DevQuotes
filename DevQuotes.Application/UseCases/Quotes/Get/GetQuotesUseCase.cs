using DevQuotes.Communication.Responses;
using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Helpers.Pagination;
using DevQuotes.Infrastructure.Repository.Quotes;
using LanguageExt.Common;
using ApplicationException = DevQuotes.Exceptions.ApplicationException;

namespace DevQuotes.Application.UseCases.Quotes.Get;

public sealed class GetQuotesUseCase(IQuotesRepository quotesRepository) : IGetQuotesUseCase
{
    private readonly IQuotesRepository _quotesRepository = quotesRepository;

    public async Task<PagedQuoteResponse> ExecuteAsync(PaginationParameters parameters, string keyword = "", CancellationToken cancellationToken = default)
    {
        string search = keyword.ToLower().Trim();
        var quotes = await _quotesRepository.GetAllAsync(parameters, x =>
            x.Language.Name.ToLower().Contains(search) ||
            x.Language.Code.ToLower().Contains(search) ||
            x.Content.ToLower().Contains(search), cancellationToken: cancellationToken);

        return new PagedQuoteResponse()
        {
            Quotes = quotes,
            Metadata = quotes.Metadata,
        };
    }

    public async Task<Result<QuoteResponse>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            var validationError = new ApplicationException();
            validationError.AddPropertyError(nameof(id), "Id cannot be empty.");
            return new Result<QuoteResponse>(validationError);
        }

        return GetResult(await _quotesRepository.GetAsync(id, cancellationToken));
    }

    public async Task<Result<QuoteResponse>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return GetResult(await _quotesRepository.GetRandomAsync(cancellationToken));
    }

    private static Result<QuoteResponse> GetResult(QuoteResponse? quote)
    {
        return quote ?? new Result<QuoteResponse>(new ApplicationException("Quote not found.", ExceptionTypes.NotFound));
    }
}
