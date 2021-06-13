using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderImport.Domain.Order.Entities;

namespace OrderImport.Infra.Data.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(e => e.Code).HasMaxLength(50).IsRequired();

            builder.HasOne(e => e.Customer).WithMany().HasForeignKey(e => e.CustomerId);
            builder.HasMany(e => e.OrderProducts).WithOne(e => e.Order);
            builder.HasOne(e => e.OrderAddress).WithOne(e => e.Order);
        }
    }
}
