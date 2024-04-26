using DevQuotes.Communication.Responses;
using DevQuotes.Infrastructure.Repository;

namespace DevQuotes.Application.UseCases.Quotes.GetLanguages;

public class GetLanguagesUseCase(IQuotesRepository quotesRepository) : IGetLanguagesUseCase
{
    private readonly IQuotesRepository _quotesRepository = quotesRepository;

    public async Task<QuoteLanguageJsonResponse> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return new QuoteLanguageJsonResponse()
        {
            Languages = await _quotesRepository.GetLanguagesAsync(cancellationToken)
        };
    }
}
