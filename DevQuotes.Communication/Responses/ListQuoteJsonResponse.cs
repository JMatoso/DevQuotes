using DevQuotes.Shared;

namespace DevQuotes.Communication.Responses;

public class ListQuoteJsonResponse
{
    public IEnumerable<QuoteJsonResponse> Quotes { get; set; } = [];
    public Metadata Metadata { get; set; } = default!;
}
