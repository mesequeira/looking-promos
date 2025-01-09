namespace ClaLookingPromos.SharedKernel.Contracts.Categories.Interfaces;

public interface ICategoryResponse
{
    public string Name { get; set; }
    
    public long ProviderCategoryId { get; set; }
}