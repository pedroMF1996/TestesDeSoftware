namespace Nerdstore.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; private set; }
        public Guid AggregateId { get; private set; }

        protected Message(string messageType, Guid aggregateId)
        {
            MessageType = messageType;
            AggregateId = aggregateId;
        }
    }
}