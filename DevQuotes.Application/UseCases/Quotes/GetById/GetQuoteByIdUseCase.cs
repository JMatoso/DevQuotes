using AutoMapper;
using DevQuotes.Communication.Responses;
using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository;
using LanguageExt.Common;
using ApplicationException = DevQuotes.Exceptions.ApplicationException;

namespace DevQuotes.Application.UseCases.Quotes.GetById;

public class GetQuoteByIdUseCase(IQuotesRepository quoteRepository, IMapper mapper) : IGetQuoteByIdUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuotesRepository _quotesRepository = quoteRepository;

    public async Task<Result<QuoteJsonResponse>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            var validationError = new ApplicationException();
            validationError.AddPropertyError(nameof(id), "Id cannot be empty.");
            return new Result<QuoteJsonResponse>(validationError);
        }

        var quote = await _quotesRepository.GetAsync(id, cancellationToken);

        return quote == null
            ? new Result<QuoteJsonResponse>(new ApplicationException("Quote not found.", ExceptionTypes.NotFound))
            : _mapper.Map<QuoteJsonResponse>(quote);
    }
}
