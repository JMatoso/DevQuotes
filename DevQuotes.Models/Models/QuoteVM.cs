namespace DevQuotes.Models.Models;

public class QuoteVM
{
    public Guid Id { get; set; }
    public string Body { get; set; } = default!;
    public string Author { get; set; } = default!;
    public DateTime Created { get; set; }
}
