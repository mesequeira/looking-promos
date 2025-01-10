using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LookingPromos.SharedKernel.Models;

public class Entity : AggregateRoot
{
    /// <summary>
    /// The main identifier of the entity.
    /// </summary>
    [BsonId]
    public ObjectId Id { get; set; } = default!;
    
    private DateTime? _createdAt;

    /// <summary>
    /// The date and time when the entity was last modified.
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// The date and time when the entity was created.
    /// </summary>
    public DateTime? CreatedAt
    {
        get => _createdAt;
        set
        {
            // Si no tiene valor, asignamos el nuevo
            if (!_createdAt.HasValue && value.HasValue)
            {
                _createdAt = value;
            }
            // Si ya tiene valor, no lo modificamos (o podrías agregar otra lógica aquí)
        }
    }
}
