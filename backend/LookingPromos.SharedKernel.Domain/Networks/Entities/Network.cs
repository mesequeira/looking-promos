using LookingPromos.SharedKernel.Domain.Categories.Entities;
using LookingPromos.SharedKernel.Models;

namespace LookingPromos.SharedKernel.Domain.Networks.Entities;

public class Network : Entity
{
    public long ExternalNetworkId { get; set; }
    public string Name { get; set; } = default!;
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}