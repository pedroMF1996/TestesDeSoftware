using MediatR;
using Nerdstore.Core.Messages;

namespace Nerdstore.Core.DomainObjects
{
    internal class DomainNotification : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }
        public DomainNotification(string key, string value) : base(nameof(DomainNotification), Guid.NewGuid())
        {
            Timestamp = DateTime.Now;
            Key = key;
            Value = value;
            Version = 1;
            DomainNotificationId = Guid.NewGuid();
        }
    }
}