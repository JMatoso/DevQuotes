using AutoMapper;
using DevQuotes.Application.Extensions;
using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;
using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository.Languages;
using LanguageExt.Common;
using ApplicationException = DevQuotes.Exceptions.ApplicationException;

namespace DevQuotes.Application.UseCases.Languages.Add;

public sealed class AddLanguageUseCase(ILanguagesRepository languageRepository, IMapper mapper) : IAddLanguageUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ILanguagesRepository _languageRepository = languageRepository;
    private readonly LanguageRequestValidator _validator = new();

    public async Task<Result<LanguageResponse>> ExecuteAsync(LanguageRequest newLanguage, CancellationToken cancellationToken = default)
    {
        var validationError = await _validator.ValidateAsync(newLanguage, cancellationToken);
        if (validationError != null)
        {
            var validationErrors = new ApplicationException();
            validationErrors.AddPropertyError("Errors", validationError.Errors.ToMappedObjects());
            return new Result<LanguageResponse>(validationErrors);
        }

        var language = _mapper.Map<Language>(newLanguage);
        var result = await _languageRepository.AddAsync(language, cancellationToken);

        if (!result.Succeeded)
        {
            var validationErrors = new ApplicationException(result.Message!, ExceptionTypes.Conflict);
            return new Result<LanguageResponse>(validationErrors);
        }

        return _mapper.Map<LanguageResponse>(language);
    }
}
