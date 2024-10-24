using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;
using DevQuotes.Shared;

namespace DevQuotes.Infrastructure.Repository.Languages
{
    public interface ILanguagesRepository
    {
        Task<Result> AddAsync(Language language, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        ValueTask<Language?> FindAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<LanguageResponse>> GetAsync(CancellationToken cancellationToken = default);
        Task<LanguageResponse?> GetByAsync(string code, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Language language, CancellationToken cancellationToken = default);
    }
}