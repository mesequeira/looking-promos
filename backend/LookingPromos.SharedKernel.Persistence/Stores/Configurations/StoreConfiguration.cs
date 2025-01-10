using LookingPromos.SharedKernel.Domain.Stores.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace LookingPromos.SharedKernel.Persistence.Stores.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToCollection("stores")
            .HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasOne(s => s.Category)
            .WithMany(c => c.Stores)
            .HasForeignKey(s => s.CategoryId);
    }
}