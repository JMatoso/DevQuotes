using System.Text.Json.Serialization;

namespace DevQuotes.Communication.Responses;

public class QuoteJsonResponse()
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid Id { get; set; } 
    public string Content { get; set; } = default!;
    public string Language { get; set; } = default!;
    public DateTime Created { get; set; }
}
