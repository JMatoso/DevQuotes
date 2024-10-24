using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Languages.Add
{
    public interface IAddLanguageUseCase
    {
        Task<Result<LanguageResponse>> ExecuteAsync(LanguageRequest newLanguage, CancellationToken cancellationToken = default);
    }
}