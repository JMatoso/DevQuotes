using DevQuotes.Extensions.Pagination;
using DevQuotes.Models;
using DevQuotes.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevQuotes.Server.Repository;

public class MockRepository<TEntity> : IMockRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly ApplicationDbContext _dbContext;

    public MockRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<bool> ObjectExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        IQueryable<TEntity> query = _dbSet;

        return await query.AnyAsync(expression);
    }

    public async Task<int> CountAsync()
    {
        return await _dbContext.Quotes.CountAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        IQueryable<TEntity> query = _dbSet;

        return expression is null ?
            await query.AsNoTracking().ToListAsync() :
            await query.AsNoTracking().Where(expression).ToListAsync();
    }

    public async Task<PagedList<TEntity>> GetAllAsync(Parameters parameters, Expression<Func<TEntity, bool>>? expression = null)
    {
        IQueryable<TEntity> query = _dbSet;

        return expression != null
                ? PagedList<TEntity>
                    .ToPagedList(await query
                        .Where(expression)
                        .ToListAsync(), parameters.Page, parameters.Limit)
                : PagedList<TEntity>
                    .ToPagedList(await query
                        .AsNoTracking()
                        .ToListAsync(), parameters.Page, parameters.Limit);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        IQueryable<TEntity> query = _dbSet;

        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(expression);
    }

    public async Task<Result> AddAsync(Expression<Func<TEntity, bool>>? predicate = null, params TEntity[] items)
    {
        IQueryable<TEntity> query = _dbSet;

        var result = new Result(successful: true);

        foreach (var item in items)
        {
            if (predicate is not null)
            {
                if (await query.AnyAsync(predicate))
                {
                    result.Successful = false;
                    result.Message = "Entity already in use.";
                    result.Code = StatusCodes.Status409Conflict;

                    return result;
                }
            }

            _dbSet.Add(item);
        }

        await _dbContext.SaveChangesAsync();
        return result;
    }

    public async Task RemoveAsync(params TEntity[] items)
    {
        foreach (var item in items)
        {
            _dbSet.Remove(item);
            _dbContext.Entry(item).State = EntityState.Deleted;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity item)
    {
        _dbSet.Attach(item);
        _dbContext.Entry(item).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync();
    }
}
