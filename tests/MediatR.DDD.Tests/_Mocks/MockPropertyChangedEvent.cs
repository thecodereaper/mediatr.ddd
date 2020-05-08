using System;

namespace MediatR.DDD.Tests._Mocks
{
    internal sealed class MockPropertyChangedEvent : IEvent
    {
        public MockPropertyChangedEvent(Guid id, string mockProperty)
        {
            Id = id;
            MockProperty = mockProperty;
        }

        public string MockProperty { get; set; }

        public Guid Id { get; set; }

        public ulong Version { get; set; }

        public DateTimeOffset TimeStamp { get; set; }
        public string Message => "Property changed.";
    }
}