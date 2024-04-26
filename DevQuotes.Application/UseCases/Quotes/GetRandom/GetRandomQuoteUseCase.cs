using AutoMapper;
using DevQuotes.Communication.Responses;
using DevQuotes.Infrastructure.Repository;

namespace DevQuotes.Application.UseCases.Quotes.GetRandom;

public class GetRandomQuoteUseCase(IMapper mapper, IQuotesRepository quotesRepository) : IGetRandomQuoteUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuotesRepository _quotesRepository = quotesRepository;

    public async Task<QuoteJsonResponse> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var quote = await _quotesRepository.GetRandomQuoteAsync(cancellationToken);
        return _mapper.Map<QuoteJsonResponse>(quote);
    }
}
