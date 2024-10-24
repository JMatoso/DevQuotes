namespace DevQuotes.Infrastructure.Helpers.Pagination;

public abstract class RequestParameters
{
    const int maxPageSize = 50;
    public int Page { get; set; } = 1;
    private int _pageSize = 40;

    public int Limit
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}

public sealed class PaginationParameters : RequestParameters { }