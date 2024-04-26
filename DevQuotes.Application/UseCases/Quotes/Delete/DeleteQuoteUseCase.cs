using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository;
using LanguageExt.Common;
using ApplicationException = DevQuotes.Exceptions.ApplicationException;

namespace DevQuotes.Application.UseCases.Quotes.Delete;

public class DeleteQuoteUseCase(IQuotesRepository quotesRepository) : IDeleteQuoteUseCase
{
    private readonly IQuotesRepository _quotesRepository = quotesRepository;

    public async Task<Result<bool>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            var validationError = new ApplicationException();
            validationError.AddPropertyError(nameof(id), "Id cannot be empty.");
            return new Result<bool>(validationError);
        }

        var result = await _quotesRepository.DeleteAsync(id, cancellationToken);

        if (!result.Succeeded)
        {
            var validationError = new ApplicationException(result.Message!);
            return new Result<bool>(validationError);
        }

        return new Result<bool>(true);
    }
}
