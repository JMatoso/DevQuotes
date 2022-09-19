using System.ComponentModel.DataAnnotations;

namespace DevQuotes.Models.Requests;

public class QuoteRequest
{
    [Required]
    [DataType(DataType.MultilineText)]
    public string Content { get; set; } = default!;
}
