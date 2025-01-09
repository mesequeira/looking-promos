using System.Text.Json.Serialization;
using ClaLookingPromos.SharedKernel.Contracts.Categories.Interfaces;

namespace ClaLookingPromos.SharedKernel.Contracts.Galicia.Responses;

public class GetGaliciaCategoriesResponse
{
    public GaliciaDataResponse Data { get; set; } = default!;
}

public class GaliciaDataResponse
{
    public int TotalSize { get; set; }
    public IEnumerable<GaliciaCategoryResponse> List { get; set; } = default!;
}

public class GaliciaCategoryResponse : ICategoryResponse
{
    public string Imagen { get; set; } = default!;
    public string Emoji { get; set; } = default!;
    public long? CategoriaPadreId { get; set; } = default!;
    [JsonPropertyName("Descripcion")] 
    public string Name { get; set; } = default!;
    [JsonPropertyName("Id")] 
    public long ProviderCategoryId { get; set; }
}