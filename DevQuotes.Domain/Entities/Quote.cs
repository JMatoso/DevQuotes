using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevQuotes.Domain.Entities;

[Table("Quotes")]
public class Quote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; private set; }

    [Required]
    [StringLength(500)]
    [DataType(DataType.MultilineText)]
    public string Content { get; set; } = default!;

    public bool IsDeleted { get; private set; }

    [Required]
    [StringLength(25)]
    public string Language { get; set; } = default!;

    [DataType(DataType.DateTime)]
    public DateTime Created { get; private set; } = DateTime.UtcNow;

    public void Delete() => IsDeleted = true;   
}
