using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediatR.DDD
{
    public interface IEventStore
    {
        Task<IEnumerable<IEvent>> Fetch(Guid id);

        Task Save(Guid id, IEnumerable<IEvent> events, ulong expectedVersion);
    }
}