using System;

namespace MediatR.DDD.Exceptions
{
    public class RuleValidationException : Exception
    {
        public RuleValidationException() { }

        public RuleValidationException(string message)
            : base(message) { }

        public RuleValidationException(string message, Exception innerException)
            : base(message, innerException) { }

        public RuleValidationException(IRule rule)
            : base($"{rule.GetType().FullName}: {rule.Message}") { }
    }
}
