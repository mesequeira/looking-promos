using LookingPromos.SharedKernel.Domain.Categories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LookingPromos.SharedKernel.Persistence.Categories.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(c => c.Stores)
            .WithOne(s => s.Category)
            .HasForeignKey(s => s.CategoryId);
    }
}