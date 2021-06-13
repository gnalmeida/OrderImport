using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderImport.Application.Core.Notifications;
using OrderImport.Application.Order.Commands;
using OrderImport.Application.Order.Dtos;
using OrderImport.Application.Order.Handlers;
using System;
using System.Threading.Tasks;

namespace OrderImport.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ApiController
    {
        public OrderController(IMediator mediator, INotificationHandler<DomainNotification> notificationHandler)
            : base(mediator, notificationHandler)
        {
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderViewModelResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetOrderQuery(id);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderViewModelResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Add(OrderViewModel orderViewModel)
        {
            //Automapper
            var addOrderCommand = new AddOrderCommand(orderViewModel.Code,
                                                      orderViewModel.Street,
                                                      orderViewModel.Number,
                                                      orderViewModel.Complement,
                                                      orderViewModel.Neighborhood,
                                                      orderViewModel.City,
                                                      orderViewModel.State,
                                                      orderViewModel.Country,
                                                      orderViewModel.PostalCode,
                                                      orderViewModel.Customer, 
                                                      orderViewModel.OrderProducts);

            var result = await _mediator.Send(addOrderCommand);

            //Automapper
            var orderViewModelResult = new OrderViewModelResult()
            {
                Id = result.Command.Id,
                Code = result.Command.Code,
                Street = result.Command.Street,
                Number = result.Command.Number,
                Complement = result.Command.Complement,
                Neighborhood = result.Command.Neighborhood,
                City = result.Command.City,
                State = result.Command.State,
                Country = result.Command.Country,
                PostalCode = result.Command.PostalCode,
                Customer = result.Command.Customer,
                OrderProducts = result.Command.OrderProducts
            };

            return Created($"{Request.Path}/{orderViewModelResult.Id}" ,orderViewModelResult);
        }
    }
}
