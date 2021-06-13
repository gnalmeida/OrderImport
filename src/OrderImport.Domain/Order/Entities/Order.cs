using OrderImport.Domain.Core.Models;
using OrderImport.Domain.Order.Interfaces;
using OrderImport.Domain.Order.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderImport.Domain.Order.Entities
{
    public class Order : Entity<Order>
    {
        protected Order() { }

        private Order(Guid customerId, string code, OrderAddress orderAddress)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Code = code;
            OrderAddress = orderAddress;
            OrderProducts = new List<OrderProduct>();
        }

        public Guid CustomerId { get; private set; }
        public string Code { get; private set; }

        public Domain.Customer.Entities.Customer Customer { get; private set; }
        public OrderAddress OrderAddress { get; private set; }
        public List<OrderProduct> OrderProducts { get; private set; }

        public void AddOrderProduct(OrderProduct orderProduct)
        {
            var item = OrderProducts.FirstOrDefault(o => o.ProductId == orderProduct.Id);
            if (item != null)
            {
                item.IncrementQuantity(orderProduct.Quantity);
            }

            this.OrderProducts.Add(orderProduct);
        }

        internal void SetOrderAddress(OrderAddress orderAddress)
        {
            OrderAddress = orderAddress;
        }

        public static Order Create(Guid customerId, string code, OrderAddress orderAddress, List<OrderProduct> orderProducts, IOrderValidation orderValidation)
        {
            var order = new Order(customerId, code, orderAddress);
            orderProducts.ForEach(op => order.AddOrderProduct(new OrderProduct(op.ProductId, op.Quantity, op.Value)));

            orderValidation.AddRuleForOrderNotExists();
            orderValidation.AddRuleForOrderProducts();

            var validation = ((OrderValidation)orderValidation).Validate(order);
            if (!validation.IsValid)
            {
                throw new DomainException(validation.Errors);
            };

            return order;
        }
    }
}
