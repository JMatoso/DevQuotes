using AutoMapper;
using DevQuotes.Communication.Responses;
using DevQuotes.Infrastructure.Helpers.Pagination;
using DevQuotes.Infrastructure.Repository;

namespace DevQuotes.Application.UseCases.Quotes.GetAll;

public class GetAllQuotesUseCase(IQuotesRepository quotesRepository, IMapper mapper) : IGetAllQuotesUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuotesRepository _quotesRepository = quotesRepository;

    public async Task<ListQuoteJsonResponse> ExecuteAsync(Parameters parameters, string keyword = "", CancellationToken cancellationToken = default)
    {
        string search = keyword.ToLower().Trim();
        var quotes = await _quotesRepository.GetAllAsync(parameters, x => x.Language.ToLower().Contains(search)
                            || x.Content.ToLower().Contains(search), cancellationToken: cancellationToken);

        return new ListQuoteJsonResponse()
        {
            Quotes = _mapper.Map<IEnumerable<QuoteJsonResponse>>(quotes),
            Metadata = quotes.Metadata,
        };
    }
}
