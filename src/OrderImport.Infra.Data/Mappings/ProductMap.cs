using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderImport.Domain.Product.Entities;

namespace OrderImport.Infra.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();

            builder.Property(e => e.Description).HasMaxLength(100).IsRequired();

            builder.Property(e => e.SKU).HasMaxLength(50).IsRequired();

            builder.Property(e => e.Value).HasPrecision(12, 2);
        }
    }
}
