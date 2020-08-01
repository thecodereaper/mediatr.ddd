using System;

namespace MediatR.DDD.Tests._Mocks
{
    public sealed class MockAggregator : AggregateRoot
    {
        public MockAggregator() { }

        public MockAggregator(Guid id)
        {
            Id = id;
        }

        public string MockProperty { get; set; }

        public void ChangeMockProperty(string mockProperty)
        {
            RaiseEvent(new MockPropertyChangedEvent(Id, mockProperty));
        }

        public void CheckBusinessRule(bool isValid)
        {
            IRule rule = new MockRule(isValid);
            CheckRule(rule);
        }

        private void Apply(MockPropertyChangedEvent e)
        {
            MockProperty = e.MockProperty;
        }
    }
}
