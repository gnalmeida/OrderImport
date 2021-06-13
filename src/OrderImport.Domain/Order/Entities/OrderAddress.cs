using OrderImport.Domain.Core.Models;
using OrderImport.Domain.Core.ValueObjects;
using System;

namespace OrderImport.Domain.Order.Entities
{
    public class OrderAddress : Entity<OrderAddress>
    {
        protected OrderAddress() { }

        public OrderAddress(Address address)
        {
            Id = Guid.NewGuid();
            this.Address = address;
        }

        public Guid OrderId { get; private set; }
        public Address Address { get; private set; }

        public Order Order { get; private set; }

        internal void SetAddress(Address address)
        {
            Address = address;
        }
    }
}
