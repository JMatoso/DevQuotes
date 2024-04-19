using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository;

namespace DevQuotes.Application.UseCases.Quotes.Delete;

public class DeleteQuoteUseCase(IQuotesRepository quotesRepository) : IDeleteQuoteUseCase
{
    private readonly IQuotesRepository _quotesRepository = quotesRepository;

    public async Task ExecuteAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ErrorOnValidationException("Id cannot be empty");

        var result = await _quotesRepository.DeleteAsync(id);

        if (!result.Succeeded)
            throw new NotFoundException(result.Message!);
    }
}
