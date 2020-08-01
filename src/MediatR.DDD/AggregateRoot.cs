using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR.DDD.Exceptions;

namespace MediatR.DDD
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly IList<IEvent> _pendingEvents = new List<IEvent>();

        public Guid Id { get; protected set; }
        public ulong Version { get; private set; }

        public void Load(IEnumerable<IEvent> events)
        {
            foreach (IEvent e in events)
            {
                if (e.Version != Version + 1)
                    throw new AggregateEventOutOfOrderException(e.Id, e.Version);

                ApplyEvent(e);

                Id = e.Id;
                Version = e.Version;
            }
        }

        public IEnumerable<IEvent> FlushPendingEvents()
        {
            lock (_pendingEvents)
            {
                IEvent[] events = _pendingEvents.ToArray();
                ulong increment = 0;

                foreach (IEvent e in events)
                {
                    if (e.Id == Guid.Empty)
                        throw new AggregateEventMissingIdException(GetType(), e.GetType());

                    ++increment;

                    e.Version = Version + increment;
                    e.TimeStamp = DateTimeOffset.UtcNow;

                    Version = e.Version;
                }

                _pendingEvents.Clear();

                return events;
            }
        }

        protected void CheckRule(IRule rule)
        {
            if (!rule.IsValid())
                throw new RuleValidationException();
        }

        protected void RaiseEvent(IEvent e)
        {
            lock (_pendingEvents)
            {
                ApplyEvent(e);
                _pendingEvents.Add(e);
            }
        }

        private void ApplyEvent(IEvent e)
        {
            GetType()
                .InvokeMember(
                    "Apply",
                    BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    this,
                    new object[] {e}
                );
        }
    }
}
