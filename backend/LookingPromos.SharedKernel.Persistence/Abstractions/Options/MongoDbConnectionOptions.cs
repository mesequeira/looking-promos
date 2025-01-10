namespace LookingPromos.SharedKernel.Persistence.Abstractions.Options;

public class MongoDbConnectionOptions
{
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
}