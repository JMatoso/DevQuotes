using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevQuotes.Models.Entities;

[Table("Quotes")]
public class Quote
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    public string Body { get; set; } = default!;

    [Required]
    public string Author { get; set; } = default!;

    public DateTime Created { get; set; }

    public Quote()
    {
        Created = DateTime.Now;
    }
}
