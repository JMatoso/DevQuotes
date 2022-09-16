using DevQuotes.Extensions.Pagination;
using DevQuotes.Models;
using System.Linq.Expressions;

namespace DevQuotes.Server.Repository;

public interface IMockRepository<TEntity> where TEntity : class
{
    Task<Result> AddAsync(Expression<Func<TEntity, bool>>? predicate = null, params TEntity[] items);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null);
    Task<PagedList<TEntity>> GetAllAsync(Parameters parameters, Expression<Func<TEntity, bool>>? expression = null);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task RemoveAsync(params TEntity[] items);
    Task<bool> ObjectExists(Expression<Func<TEntity, bool>> expression);
    Task UpdateAsync(TEntity item);
}
