using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderImport.Domain.Order.Entities;

namespace OrderImport.Infra.Data.Mappings
{
    public class OrderAddressMap : IEntityTypeConfiguration<OrderAddress>
    {
        public void Configure(EntityTypeBuilder<OrderAddress> builder)
        {
            builder.HasKey(s => s.Id);

            builder.OwnsOne(e => e.Address).Property(e => e.Street).HasColumnName("Street").HasMaxLength(100).IsRequired();
            builder.OwnsOne(e => e.Address).Property(e => e.Number).HasColumnName("Number").HasMaxLength(20).IsRequired();
            builder.OwnsOne(e => e.Address).Property(e => e.Complement).HasColumnName("Complement").HasMaxLength(50).IsRequired();
            builder.OwnsOne(e => e.Address).Property(e => e.Neighborhood).HasColumnName("Neighborhood").HasMaxLength(50).IsRequired();
            builder.OwnsOne(e => e.Address).Property(e => e.City).HasColumnName("City").HasMaxLength(50).IsRequired();
            builder.OwnsOne(e => e.Address).Property(e => e.State).HasColumnName("State").HasMaxLength(50).IsRequired();
            builder.OwnsOne(e => e.Address).Property(e => e.Country).HasColumnName("Country").HasMaxLength(50).IsRequired();
        }
    }
}
