using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderImport.Application.Core.Notifications;
using OrderImport.Application.Product.Commands;
using OrderImport.Application.Product.Dtos;
using System.Threading.Tasks;

namespace OrderImport.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ApiController
    {
        public ProductController(IMediator mediator, INotificationHandler<DomainNotification> notificationHandler)
            : base(mediator, notificationHandler)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Add(ProductViewModel productViewModel)
        {
            var addProductCommand = new AddProductCommand(productViewModel.SKU,
                                              productViewModel.Name,
                                              productViewModel.Value,
                                              productViewModel.Description);

            await _mediator.Send(addProductCommand);

            return Created($"{Request.Path}/{addProductCommand.Id}", productViewModel);
        }
    }
}
