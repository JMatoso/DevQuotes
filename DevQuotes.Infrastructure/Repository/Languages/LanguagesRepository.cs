using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;
using DevQuotes.Shared;
using Microsoft.EntityFrameworkCore;

namespace DevQuotes.Infrastructure.Repository.Languages;

public sealed class LanguagesRepository(ApplicationDbContext dbContext) : ILanguagesRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async ValueTask<Language?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Languages.FindAsync([id], cancellationToken);
    }

    public async Task<List<LanguageResponse>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Languages.AsNoTracking().Select(x => new LanguageResponse()
        {
            Name = x.Name,
            Code = x.Code,
        }).ToListAsync(cancellationToken);
    }

    public async Task<LanguageResponse?> GetByAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Languages.AsNoTracking()
            .Select(x => new LanguageResponse()
            {
                Name = x.Name,
                Code = x.Code,
                Quotes = x.Quotes.Select(q => new QuoteResponse()
                {
                    Content = q.Content
                }).ToList(),
            }).FirstOrDefaultAsync(x => x.Code.Equals(code), cancellationToken);
    }

    public async Task<Result> AddAsync(Language language, CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Languages.AnyAsync(x => x.Name.Equals(language.Name) && x.Code.Equals(language.Code), cancellationToken))
        {
            return Result.Fail("Language already exists.");
        }

        await _dbContext.Languages.AddAsync(language, cancellationToken);
        return await _dbContext.SaveAsync(cancellationToken: cancellationToken);
    }

    public async Task<Result> UpdateAsync(Language language, CancellationToken cancellationToken = default)
    {
        _dbContext.Languages.Update(language);
        return await _dbContext.SaveAsync(cancellationToken: cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Languages.Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(y => y.IsDeleted, true), cancellationToken);
        return result > 0 ? Result.Success() : Result.Fail("No changes were made.");
    }
}
