namespace DevQuotes.Infrastructure.Helpers.Pagination;

public sealed class Pagination
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 40;
}
