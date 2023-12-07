using Nerdstore.Core.Messages;
using System.Collections.ObjectModel;

namespace Nerdstore.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications.AsReadOnly();
        protected Entity() 
        {
            Id = Guid.NewGuid();
        }

        public void AddNotification(Event notification){
            _notifications ??= new List<Event>();
            _notifications.Add(notification);
        }

        public void ClearNotifications()
        {
            _notifications.Clear();
        }
    }
}
