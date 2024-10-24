using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevQuotes.Domain.Entities;

[Table("Languages")]
public class Language : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(5)]
    public string Code { get; set; } = string.Empty;

    public virtual ICollection<Quote> Quotes { get; set; } = [];
}
