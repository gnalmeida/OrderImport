using OrderImport.Domain.Core.Models;
using System;

namespace OrderImport.Domain.Order.Entities
{
    public class OrderProduct : Entity<OrderProduct>
    {
        protected OrderProduct() { }

        public OrderProduct(Guid productId, int quantity, decimal value)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            Value = value;
        }

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; private set; }
        public decimal Value { get; private set; }
        public decimal Total => Quantity * Value;

        public Order Order { get; private set; }
        public Domain.Product.Entities.Product Product { get; private set; }

        internal void IncrementQuantity(int quantity)
        {
            this.Quantity += quantity;
        }
    }
}
