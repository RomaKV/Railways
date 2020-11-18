using System;

namespace RabbitMQ.Events
{
    public class UpdateInfoStationsEvent : IEvent
    {
        public DateTime CreatedAt { get; }

        public UpdateInfoStationsEvent( DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
    }
}