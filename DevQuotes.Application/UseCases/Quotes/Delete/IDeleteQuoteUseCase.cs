
namespace DevQuotes.Application.UseCases.Quotes.Delete
{
    public interface IDeleteQuoteUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}