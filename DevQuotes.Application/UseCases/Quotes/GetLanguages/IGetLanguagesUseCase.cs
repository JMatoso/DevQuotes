using DevQuotes.Communication.Responses;

namespace DevQuotes.Application.UseCases.Quotes.GetLanguages
{
    public interface IGetLanguagesUseCase
    {
        Task<QuoteLanguageJsonResponse> ExecuteAsync();
    }
}