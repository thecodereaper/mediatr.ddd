using System;
using System.Collections.Generic;

namespace MediatR.DDD
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        ulong Version { get; }

        void Load(IEnumerable<IEvent> events);

        IEnumerable<IEvent> FlushPendingEvents();
    }
}