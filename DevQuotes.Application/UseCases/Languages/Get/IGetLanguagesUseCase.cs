using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Languages.Get
{
    public interface IGetLanguagesUseCase
    {
        Task<List<LanguageResponse>> ExecuteAsync(CancellationToken cancellationToken = default);
        Task<Language?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<LanguageResponse>> ExecuteAsync(string code, CancellationToken cancellationToken = default);
    }
}