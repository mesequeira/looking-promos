using System.Text.Json.Serialization;
using ClaLookingPromos.SharedKernel.Contracts.Stores;

namespace ClaLookingPromos.SharedKernel.Contracts.Galicia.Responses;

public class GetGaliciaStoresResponse
{
    public long ProviderCategoryId { get; set; }
    public GaliciaStoresDataResponse Data { get; set; } = default!;
}

public class GaliciaStoresDataResponse
{
    public int TotalSize { get; set; }
    public IEnumerable<StoreResponse> List { get; set; } = default!;
}

public class StoreResponse : IStoreResponse
{
    [JsonPropertyName("Titulo")]
    public string Name { get; set; } = default!;
    
    [JsonPropertyName("MarcaId")]
    public long ExternalStoreId { get; set; }
    
    public long CategoryId { get; set; }
}