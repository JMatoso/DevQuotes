using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DevQuotes.Communication.Responses;

public sealed class QuoteResponse()
{

    [DataType(DataType.Html)]
    public string Content { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public LanguageResponse Language { get; set; } = default!;
}
