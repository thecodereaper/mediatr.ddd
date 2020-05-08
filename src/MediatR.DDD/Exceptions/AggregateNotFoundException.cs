using System;

namespace MediatR.DDD.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException() { }

        public AggregateNotFoundException(string message)
            : base(message) { }

        public AggregateNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        public AggregateNotFoundException(Guid id)
            : base($"Aggregate ID: {id} - Not found.") { }
    }
}