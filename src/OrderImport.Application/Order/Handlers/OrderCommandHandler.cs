using MediatR;
using OrderImport.Application.Core.Commands;
using OrderImport.Application.Customer.Commands;
using OrderImport.Application.Customer.Dtos;
using OrderImport.Application.Order.Commands;
using OrderImport.Application.Product.Commands;
using OrderImport.Application.Product.Dtos;
using OrderImport.Domain.Core.Interfaces;
using OrderImport.Domain.Core.Models;
using OrderImport.Domain.Core.ValueObjects;
using OrderImport.Domain.Order.Entities;
using OrderImport.Domain.Order.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderImport.Application.Order.Handlers
{
    public class OrderCommandHandler : CommandHandlerBase, ICommandHandler<AddOrderCommand, Result<AddOrderCommand>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderValidation _orderValidation;
        private readonly IMediator _mediator;

        public OrderCommandHandler(IOrderRepository orderRepository,
                                   IOrderValidation orderValidation,
                                   IMediator mediator,
                                   IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderValidation = orderValidation;
            _mediator = mediator;
        }

        public async Task<Result<AddOrderCommand>> Handle(AddOrderCommand command, CancellationToken cancellationToken)
        {
            var customerId = await AddCustomerIfNotExists(command.Customer);

            var result = await AddOrder(command, customerId);

            await Commit(result);

            return result;
        }

        private async Task<Result<AddOrderCommand>> AddOrder(AddOrderCommand command, Guid customerId)
        {
            var result = new Result<AddOrderCommand>(command);

            var orderAdress = new OrderAddress(new Address(command.Street,
                                                           command.Number,
                                                           command.Complement,
                                                           command.Neighborhood,
                                                           command.City,
                                                           command.Country,
                                                           command.Country,
                                                           command.PostalCode));

            var orderProducts = new List<OrderProduct>();
            foreach (var item in command.OrderProducts)
            {
                var productId = await AddProductIfNotExists(item);

                var orderProduct = new OrderProduct(productId, item.Quantity, item.Value);

                orderProducts.Add(orderProduct);
            }

            var order = Domain.Order.Entities.Order.Create(customerId,
                                                           command.Code,
                                                           orderAdress,
                                                           orderProducts,
                                                           _orderValidation);

            await _orderRepository.AddAsync(order);

            result.Command.SetId(order.Id);

            return result;
        }

        private async Task<Guid> AddCustomerIfNotExists(CustomerViewModel customerViewModel)
        {
            var addCustomerIfNotExistsCommand = new AddCustomerIfNotExistsCommand(new AddCustomerCommand(customerViewModel.Name, customerViewModel.CPF));

            var result = await _mediator.Send(addCustomerIfNotExistsCommand);

            return result.Command.AddCustomerCommand.Id;
        }

        private async Task<Guid> AddProductIfNotExists(ProductViewModel productViewModel)
        {
            var addProductCommand = new AddProductCommand(productViewModel.SKU,
                                                          productViewModel.Name,
                                                          productViewModel.Value,
                                                          productViewModel.Description);

            var addProductIfNotExistCommand = new AddProductIfNotExistCommand(addProductCommand);

            var result = await _mediator.Send(addProductIfNotExistCommand);

            return result.Command.AddProductCommand.Id;
        }
    }
}
