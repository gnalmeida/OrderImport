using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderImport.Application.Core.Notifications
{
    public interface IDomainNotificationHandler
    {
        void Dispose();
        List<DomainNotification> GetNotifications();
        Task Handle(DomainNotification notification, CancellationToken cancellationToken);
        bool HasNotifications();
    }

    public class DomainNotificationHandler : INotificationHandler<DomainNotification>, IDomainNotificationHandler
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);

            return Task.CompletedTask;
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}