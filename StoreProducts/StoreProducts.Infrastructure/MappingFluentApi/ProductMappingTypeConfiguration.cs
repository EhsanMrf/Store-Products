using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreProducts.Infrastructure.MappingFluentApi;

public class ProductMappingTypeConfiguration : IEntityTypeConfiguration<Core.Product.Entity.Product>
{
    public void Configure(EntityTypeBuilder<Core.Product.Entity.Product> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(150);
        builder.Property(x => x.ManufacturePhone).HasMaxLength(20);
        builder.Property(x => x.ManufactureEmail).HasMaxLength(40);
        builder.HasIndex(x => x.ManufactureEmail).IsUnique();
        builder.HasIndex(x=>x.ProduceDate).IsUnique();
    }
}