using System;

namespace OrderImport.Application.Order.Commands
{
    public class AddOrderProductCommand
    {
        public AddOrderProductCommand(Guid productId, int quantity, decimal value)
        {
            ProductId = productId;
            Quantity = quantity;
            Value = value;
        }

        public Guid ProductId { get; set; }
        public int Quantity { get; private set; }
        public decimal Value { get; private set; }
        public decimal Total => Quantity * Value;
    }
}
