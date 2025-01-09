using LookingPromos.SharedKernel.Domain.Networks.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LookingPromos.SharedKernel.Persistence.Networks.Configurations;

public class NetworkConfiguration : IEntityTypeConfiguration<Network>
{
    public void Configure(EntityTypeBuilder<Network> builder)
    {
        // Configuración para Network
        builder
            .ToTable("Networks")
            .HasKey(n => n.Id);

        builder
            .Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasMany(n => n.Categories)
            .WithOne(c => c.Network)
            .HasForeignKey(c => c.NetworkId);
    }
}