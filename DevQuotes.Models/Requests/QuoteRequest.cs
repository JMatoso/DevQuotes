using System.ComponentModel.DataAnnotations;

namespace DevQuotes.Models.Requests;

public class QuoteRequest
{
    [Required]
    [DataType(DataType.MultilineText)]
    public string Body { get; set; } = default!;

    [Required]
    public string Author { get; set; } = default!;
}
