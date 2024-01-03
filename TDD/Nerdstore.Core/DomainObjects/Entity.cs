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

        public void AdicionarEvento(Event notification){
            _notifications ??= new List<Event>();
            _notifications.Add(notification);
        }

        public void RemoverEvento(Event notification) 
        {
            _notifications.Remove(notification);
        }

        public void LimparEventos()
        {
            _notifications.Clear();
        }
    }
}
