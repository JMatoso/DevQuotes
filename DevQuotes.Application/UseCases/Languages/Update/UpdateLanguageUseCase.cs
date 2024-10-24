using AutoMapper;
using DevQuotes.Application.Extensions;
using DevQuotes.Communication.Requests;
using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository.Languages;
using LanguageExt.Common;
using ApplicationException = DevQuotes.Exceptions.ApplicationException;

namespace DevQuotes.Application.UseCases.Languages.Update;

public sealed class UpdateLanguageUseCase(ILanguagesRepository languagesRepository, IMapper mapper) : IUpdateLanguageUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ILanguagesRepository _languagesRepository = languagesRepository;
    private readonly LanguageRequestValidator _validator = new();

    public async Task<Result<bool>> ExecuteAsync(Guid id, LanguageRequest language, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            var validationError = new ApplicationException();
            validationError.AddPropertyError(nameof(id), "Id cannot be empty.");
            return new Result<bool>(validationError);
        }

        var languageToUpdate = await _languagesRepository.FindAsync(id, cancellationToken);
        if (languageToUpdate is null)
        {
            return new Result<bool>(new ApplicationException("Language not found", ExceptionTypes.NotFound));
        }

        var validationResult = await _validator.ValidateAsync(language, cancellationToken);
        if (!validationResult.IsValid)
        {
            var validationErrors = new ApplicationException();
            validationErrors.AddPropertyError("Errors", validationResult.Errors.ToMappedObjects());
            return new Result<bool>(validationErrors);
        }

        _ = _mapper.Map(language, languageToUpdate);
        var result = await _languagesRepository.UpdateAsync(languageToUpdate, cancellationToken);

        if (!result.Succeeded)
        {
            return new Result<bool>(new ApplicationException(result.Message!, ExceptionTypes.Conflict));
        }

        return new Result<bool>(true);
    }
}
