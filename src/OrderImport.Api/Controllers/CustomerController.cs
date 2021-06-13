using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderImport.Application.Core.Notifications;
using OrderImport.Application.Customer.Commands;
using OrderImport.Application.Customer.Dtos;
using OrderImport.Application.Product.Dtos;
using System.Threading.Tasks;

namespace OrderImport.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ApiController
    {
        public CustomerController(IMediator mediator, INotificationHandler<DomainNotification> notificationHandler)
            : base(mediator, notificationHandler)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Add(CustomerViewModel customerViewModel)
        {
            var addCustomerCommand = new AddCustomerCommand(customerViewModel.Name, customerViewModel.CPF);

            await _mediator.Send(addCustomerCommand);

            return Created($"{Request.Path}/{addCustomerCommand.Id}", customerViewModel);
        }
    }
}
