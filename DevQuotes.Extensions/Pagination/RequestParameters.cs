namespace DevQuotes.Extensions.Pagination;

public abstract class RequestParameters
{
    const int maxPageSize = 25;
    public int Page { get; set; } = 1;
    private int _pageSize = 10;

    public int Limit
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}

public class Parameters : RequestParameters { }
