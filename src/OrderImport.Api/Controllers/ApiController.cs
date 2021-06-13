using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OrderImport.Application.Core.Notifications;
using OrderImport.Api.Filters;

namespace OrderImport.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ValidateModelStateAttribute]
    public abstract class ApiController : ControllerBase
    {
        protected readonly DomainNotificationHandler _domainNotificationHandler;
        protected readonly IMediator _mediator;

        protected ApiController(IMediator mediator, INotificationHandler<DomainNotification> notificationHandler)
        {
            _mediator = mediator;
            _domainNotificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }                       

            return BadRequest(new
            {
                errors = _domainNotificationHandler.GetNotifications().Select(n => n.Value)
            });
        }

        protected bool IsValidOperation()
        {
            return !_domainNotificationHandler.HasNotifications();
        }

        protected void NotifyInvalidModel()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                var erroMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.Publish(new DomainNotification(code, message));
        }
    }
}