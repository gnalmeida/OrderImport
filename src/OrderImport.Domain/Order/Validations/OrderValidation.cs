using FluentValidation;
using OrderImport.Domain.Order.Interfaces;
using OrderImport.Domain.Order.Specifications;
using System.Linq;

namespace OrderImport.Domain.Order.Validations
{
    public class OrderValidation : AbstractValidator<Entities.Order>, IOrderValidation
    {
        private readonly IOrderRepository _orderRepository;

        public OrderValidation(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void AddRuleForOrderNotExists()
        {
            var spec = new OrderNotExistsSpecification(_orderRepository);

            RuleFor(c => c).Must(spec.IsSatisfiedBy).WithMessage("Pedido já cadastrado!");
        }

        public void AddRuleForOrderProducts()
        {
            RuleFor(c => c.OrderProducts.Count).GreaterThan(0).WithMessage("Pedido sem produtos!");
            RuleFor(c => c.OrderProducts).Must(o => !o.Any(o => o.Quantity == 0)).WithMessage("O Pedido possui itens com quantidade zerada");
        }
    }
}