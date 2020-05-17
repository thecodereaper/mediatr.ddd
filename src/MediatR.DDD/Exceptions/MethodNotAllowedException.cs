using System;

namespace MediatR.DDD.Exceptions
{
    public class MethodNotAllowedException : Exception
    {
        public MethodNotAllowedException() { }

        public MethodNotAllowedException(string message)
            : base(message) { }

        public MethodNotAllowedException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}