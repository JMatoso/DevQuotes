using DevQuotes.Infrastructure.Repository.Languages;
using LanguageExt.Common;
using ApplicationException = DevQuotes.Exceptions.ApplicationException;

namespace DevQuotes.Application.UseCases.Languages.Delete;

public sealed class DeleteLanguageUseCase(ILanguagesRepository languageRepository) : IDeleteLanguageUseCase
{
    private readonly ILanguagesRepository _languageRepository = languageRepository;

    public async Task<Result<bool>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            var validationError = new ApplicationException();
            validationError.AddPropertyError(nameof(id), "Id cannot be empty.");
            return new Result<bool>(validationError);
        }

        var result = await _languageRepository.DeleteAsync(id, cancellationToken);

        if (!result.Succeeded)
        {
            var validationError = new ApplicationException(result.Message!);
            return new Result<bool>(validationError);
        }

        return new Result<bool>(true);
    }
}
