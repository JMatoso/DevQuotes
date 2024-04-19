using DevQuotes.Shared;
using Microsoft.EntityFrameworkCore;

namespace DevQuotes.Infrastructure.Helpers.Pagination;

public class PagedList<TEntity> : List<TEntity>
{
    public Metadata Metadata { get; set; }
    public PagedList(List<TEntity> items, int count, int pageNumber, int pageSize)
    {
        Metadata = new Metadata
        {
            TotalItems = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            Count = (int)Math.Ceiling(count / (double)pageSize)
        };

        AddRange(items);
    }

    public static async Task<PagedList<TEntity>> ToPagedList(IQueryable<TEntity> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        return new PagedList<TEntity>(items, count, pageNumber, pageSize);
    }
}

