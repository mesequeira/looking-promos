namespace ClaLookingPromos.SharedKernel.Contracts.Stores;

public interface IStoreResponse
{
    public string Name { get; set; }

    public long ExternalStoreId { get; set; }

    public long CategoryId { get; set; }
}