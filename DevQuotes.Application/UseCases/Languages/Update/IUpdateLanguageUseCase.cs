using DevQuotes.Communication.Requests;
using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Languages.Update
{
    public interface IUpdateLanguageUseCase
    {
        Task<Result<bool>> ExecuteAsync(Guid id, LanguageRequest language, CancellationToken cancellationToken = default);
    }
}