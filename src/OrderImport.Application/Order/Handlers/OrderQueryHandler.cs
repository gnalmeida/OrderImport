using OrderImport.Application.Core.Queries;
using OrderImport.Application.Order.Dtos;
using OrderImport.Domain.Order.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace OrderImport.Application.Order.Handlers
{
    public class OrderQueryHandler : IQueryHandler<GetOrderQuery, OrderViewModelResult>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderViewModelResult> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.Id);

            var orderViewModelResult = new OrderViewModelResult()
            {
                Id = order.Id,
                Code = order.Code
            };

            return orderViewModelResult;
        }
    }
}
