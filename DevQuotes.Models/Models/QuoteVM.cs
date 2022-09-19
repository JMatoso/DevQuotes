using MongoDB.Bson;

namespace DevQuotes.Models.Models;

public class QuoteVM
{
    public string Id { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime Created { get; set; }
}
