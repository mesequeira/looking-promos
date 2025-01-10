using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace LookingPromos.SharedKernel.Domain.Networks.Entities;

public class Network : Entity
{
    
    [BsonElement("type")]
    public string? Type { get; set; } = default!;

    [BsonElement("description")]
    public string? Description { get; set; }
    
    [BsonElement("name")]
    public string? Name { get; set; } = default!;
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}