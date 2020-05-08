using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using MediatR.DDD.Tests._Mocks;
using Moq;
using Xunit;

namespace MediatR.DDD.Tests
{
    public class RepositoryTests
    {
        public RepositoryTests()
        {
            _mockEventStore = new Mock<IEventStore>();
            _mockMediator = new Mock<IMediator>();

            _repository = new Repository<MockAggregator>(_mockEventStore.Object, _mockMediator.Object);
        }

        private readonly IRepository<MockAggregator> _repository;
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<IEventStore> _mockEventStore;
        private readonly Mock<IMediator> _mockMediator;

        [Fact]
        public async Task Fetch_WhenAnAggregatorIsSaved_ThenTheEventsShouldBeFlushed()
        {
            Mock<IAggregateRoot> mockAggregate = new Mock<IAggregateRoot>();

            _mockEventStore.Setup(call => call.Save(It.IsAny<Guid>(), It.IsAny<IEnumerable<IEvent>>(), It.IsAny<ulong>())).Returns(Task.CompletedTask);

            await _repository.SaveAsync(mockAggregate.Object);

            mockAggregate.VerifyGet(call => call.Id, Times.Once);
            mockAggregate.Verify(call => call.FlushPendingEvents(), Times.Once);
            _mockEventStore.VerifyAll();
        }

        [Fact]
        public void Fetch_WhenTheEventsFromTheEventStoreAreFetchedAndLoadedToTheAggregator_ThenTheAggregatorShouldBeReturnedWithTheEventsLoaded()
        {
            Guid id = _fixture.Create<Guid>();
            string mockProperty = _fixture.Create<string>();
            IEvent e = new MockPropertyChangedEvent(id, mockProperty) {Version = 1};
            _mockEventStore.Setup(call => call.Fetch(id)).Returns(Task.FromResult<IEnumerable<IEvent>>(new[] {e}));

            MockAggregator actualAggregator = _repository.FetchAsync(id).Result;

            actualAggregator.Id.Should().Be(id);
            actualAggregator.MockProperty.Should().Be(mockProperty);
            actualAggregator.Version.Should().Be(e.Version);
        }
    }
}