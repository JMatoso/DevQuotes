using DevQuotes.Communication.Responses;
using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository.Languages;
using LanguageExt.Common;
using ApplicationException = DevQuotes.Exceptions.ApplicationException;

namespace DevQuotes.Application.UseCases.Languages.Get;

public sealed class GetLanguagesUseCase(ILanguagesRepository languageRepository) : IGetLanguagesUseCase
{
    private readonly ILanguagesRepository _languageRepository = languageRepository;

    public async Task<List<LanguageResponse>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _languageRepository.GetAsync(cancellationToken);
    }

    public async Task<Result<LanguageResponse>> ExecuteAsync(Guid id, bool includeQuotes, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            var validationError = new ApplicationException();
            validationError.AddPropertyError(nameof(id), "Id cannot be empty.");
            return new Result<LanguageResponse>(validationError);
        }

        return GetResult(await _languageRepository.GetAsync(id, includeQuotes, cancellationToken));
    }

    public async Task<Result<LanguageResponse>> ExecuteAsync(string name, string code, CancellationToken cancellationToken = default)
    {
        return GetResult(await _languageRepository.GetByAsync(name, code, cancellationToken));
    }

    private static Result<LanguageResponse> GetResult(LanguageResponse? language)
    {
        return language ?? new Result<LanguageResponse>(new ApplicationException("Language not found.", ExceptionTypes.NotFound));
    }
}
