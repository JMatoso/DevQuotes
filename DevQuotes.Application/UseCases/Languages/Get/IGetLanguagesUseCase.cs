using DevQuotes.Communication.Responses;
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Languages.Get
{
    public interface IGetLanguagesUseCase
    {
        Task<List<LanguageResponse>> ExecuteAsync(CancellationToken cancellationToken = default);
        Task<Result<LanguageResponse>> ExecuteAsync(Guid id, bool includeQuotes, CancellationToken cancellationToken = default);
        Task<Result<LanguageResponse>> ExecuteAsync(string name, string code, CancellationToken cancellationToken = default);
    }
}