using AutoMapper;
using DevQuotes.Communication.Responses;
using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository;

namespace DevQuotes.Application.UseCases.Quotes.GetById;

public class GetQuoteByIdUseCase(IQuotesRepository quoteRepository, IMapper mapper) : IGetQuoteByIdUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuotesRepository _quotesRepository = quoteRepository;

    public async Task<QuoteJsonResponse> ExecuteAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ErrorOnValidationException("Id cannot be empty");
        }

        var quote = await _quotesRepository.GetAsync(id);

        return quote == null
            ? throw new NotFoundException("Quote not found")
            : _mapper.Map<QuoteJsonResponse>(quote);
    }
}
