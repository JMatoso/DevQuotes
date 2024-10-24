using DevQuotes.Shared;

namespace DevQuotes.Communication.Responses;

public sealed class PagedQuoteResponse
{
    public List<QuoteResponse> Quotes { get; set; } = [];
    public Metadata Metadata { get; set; } = default!;
}
