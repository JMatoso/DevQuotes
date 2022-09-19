using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevQuotes.Models.Entities;

[Table("Quotes")]
public class Quote
{
    [Key]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    public string Content { get; set; } = default!;

    public DateTime Created { get; set; }

    public Quote()
    {
        Id = ObjectId.GenerateNewId().ToString();
        Created = DateTime.Now;
    }
}
