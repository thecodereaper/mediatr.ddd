using System;

namespace MediatR.DDD.Exceptions
{
    public class AggregateEventOutOfOrderException : Exception
    {
        public AggregateEventOutOfOrderException() { }

        public AggregateEventOutOfOrderException(string message)
            : base(message) { }

        public AggregateEventOutOfOrderException(string message, Exception innerException)
            : base(message, innerException) { }

        public AggregateEventOutOfOrderException(Guid id, ulong version)
            : base($"Aggregate ID: {id} - Event with version {version} is out of order.") { }
    }
}