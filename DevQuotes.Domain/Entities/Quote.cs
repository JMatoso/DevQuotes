using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevQuotes.Domain.Entities;

[Table("Quotes")]
public class Quote : BaseEntity
{
    [Required]
    [StringLength(2500)]
    public string Content { get; set; } = string.Empty;

    [Required]
    public Guid? LanguageId { get; set; }

    [ForeignKey(nameof(LanguageId))]
    public virtual Language Language { get; set; } = default!;   
}
