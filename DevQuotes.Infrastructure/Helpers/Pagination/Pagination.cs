namespace DevQuotes.Infrastructure.Helpers.Pagination;

public class Pagination
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 40;

    [Obsolete("Not Implemented")]
    public string OrderBy { get; set; } = "Created";
}
