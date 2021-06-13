using OrderImport.Domain.Core.Specifications;
using OrderImport.Domain.Order.Interfaces;
using System.Linq;

namespace OrderImport.Domain.Order.Specifications
{
    public class OrderNotExistsSpecification : BaseSpecification<Order.Entities.Order>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderNotExistsSpecification(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public override bool IsSatisfiedBy(Entities.Order order)
        {
            return !_orderRepository.FindAsync(p => p.Code == order.Code).Result.Any();
        }
    }
}