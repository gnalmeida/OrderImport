using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderImport.Domain.Customer.Entities;

namespace OrderImport.Infra.Data.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();

            builder.OwnsOne(e => e.CPF).Property(e => e.Number).HasColumnName("CPF").HasMaxLength(11).IsRequired();
        }
    }
}
