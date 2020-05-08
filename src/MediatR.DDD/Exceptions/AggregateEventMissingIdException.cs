using System;

namespace MediatR.DDD.Exceptions
{
    public class AggregateEventMissingIdException : Exception
    {
        public AggregateEventMissingIdException() { }

        public AggregateEventMissingIdException(string message)
            : base(message) { }

        public AggregateEventMissingIdException(string message, Exception innerException)
            : base(message, innerException) { }

        public AggregateEventMissingIdException(Type aggregateType, Type eventType)
            : base($"Attempted to save event of type {eventType.FullName} from {aggregateType.FullName} but no IDs where set.") { }
    }
}