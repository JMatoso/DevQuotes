using System.Text.Json.Serialization;

namespace DevQuotes.Communication.Responses;

public sealed class LanguageResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<QuoteResponse> Quotes { get; set; } = [];

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime CreatedAt { get; set; } = default;
}