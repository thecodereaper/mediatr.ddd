using System;
using System.Threading.Tasks;

namespace MediatR.DDD
{
    public interface IRepository<T>
        where T : IAggregateRoot, new()
    {
        Task<T> FetchAsync(Guid id);

        Task SaveAsync(IAggregateRoot aggregate);
    }
}