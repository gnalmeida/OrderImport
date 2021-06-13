using MediatR;

namespace OrderImport.Application.Core.Notifications
{
    public class Event : INotification, IRequest<bool>
    {

    }
}