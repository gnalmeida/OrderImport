using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderImport.Domain.Order.Entities;

namespace OrderImport.Infra.Data.Mappings
{
    public class OrderProductMap : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(e => e.Value).HasPrecision(12, 2);

            builder.HasOne(e => e.Product).WithMany().HasForeignKey(e => e.ProductId);
        }
    }
}
