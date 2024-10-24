using LanguageExt.Common;

namespace DevQuotes.Application.UseCases.Languages.Delete
{
    public interface IDeleteLanguageUseCase
    {
        Task<Result<bool>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}