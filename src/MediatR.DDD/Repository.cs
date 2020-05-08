using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR.DDD
{
    public class Repository<T> : IRepository<T>
        where T : IAggregateRoot, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IMediator _mediator;

        public Repository(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public async Task<T> FetchAsync(Guid id)
        {
            T aggregate = new T();

            IEnumerable<IEvent> eventHistory = await _eventStore.Fetch(id);
            aggregate.Load(eventHistory);

            return aggregate;
        }

        public async Task SaveAsync(IAggregateRoot aggregate)
        {
            IList<IEvent> events = aggregate.FlushPendingEvents().ToList();

            await _eventStore.Save(aggregate.Id, events, aggregate.Version);

            foreach (IEvent e in events) await _mediator.Publish(e);
        }
    }
}