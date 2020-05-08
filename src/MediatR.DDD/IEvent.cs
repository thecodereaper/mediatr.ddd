using System;

namespace MediatR.DDD
{
    public interface IEvent : INotification
    {
        Guid Id { get; }
        ulong Version { get; set; }
        DateTimeOffset TimeStamp { get; set; }
        string Message { get; }
    }
}