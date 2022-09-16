namespace DevQuotes.Extensions.Pagination;

public abstract class RequestParameters
{
    const int maxPageSize = 50;
    public int Page { get; set; } = 1;
    private int _pageSize = 20;

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
