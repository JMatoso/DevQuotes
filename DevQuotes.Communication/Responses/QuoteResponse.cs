using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DevQuotes.Communication.Responses;

public sealed class QuoteResponse()
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid Id { get; set; }

    [DataType(DataType.Html)]
    public string Content { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public LanguageResponse Language { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime CreatedAt { get; set; } = default;
}
