using System.Text.Json.Serialization;

namespace DevQuotes.Communication.Responses;

public sealed class LanguageResponse
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<QuoteResponse> Quotes { get; set; } = default!;
}