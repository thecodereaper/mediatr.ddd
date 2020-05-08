using System;

namespace MediatR.DDD.Exceptions
{
    public class AggregateConcurrencyException : Exception
    {
        public AggregateConcurrencyException() { }

        public AggregateConcurrencyException(string message)
            : base(message) { }

        public AggregateConcurrencyException(string message, Exception innerException)
            : base(message, innerException) { }

        public AggregateConcurrencyException(Guid id)
            : base($"Aggregate ID: {id} - Data has been changed between loading and state changes.") { }
    }
}