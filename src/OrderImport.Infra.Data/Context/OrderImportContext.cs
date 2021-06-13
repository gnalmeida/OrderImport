using Microsoft.EntityFrameworkCore;
using OrderImport.Infra.Data.Mappings;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderImport.Infra.Data.Context
{
    public class OrderImportContext : DbContext
    {
        public OrderImportContext(DbContextOptions<OrderImportContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Customer.Entities.Customer> Customer { get; set; }
        public DbSet<Domain.Product.Entities.Product> Product { get; set; }
        public DbSet<Domain.Order.Entities.Order> Order { get; set; }
        public DbSet<Domain.Order.Entities.OrderAddress> OrderAddress { get; set; }
        public DbSet<Domain.Order.Entities.OrderProduct> OrderProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderProductMap());
            modelBuilder.ApplyConfiguration(new OrderAddressMap());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetCreatedAndUpdatedDates();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetCreatedAndUpdatedDates();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetCreatedAndUpdatedDates()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Created") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = DateTime.Now;
                    entry.Property("Updated").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("Updated").CurrentValue = DateTime.Now;
                }
            }
        }
    }

}
