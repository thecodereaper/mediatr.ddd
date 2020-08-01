using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using MediatR.DDD.Exceptions;
using MediatR.DDD.Tests._Mocks;
using Xbehave;

namespace MediatR.DDD.Tests
{
    public sealed class AggregateFeature
    {
        private readonly IFixture _fixture = new Fixture();

        [Scenario]
        public void RaiseEvent_WhenAPropertyIsChangedInAnAggregator_ThenAnEventShouldBeRaisedAndTheChangeShouldBeAppliedToTheAggregator
        (
            Guid id,
            string mockProperty,
            MockAggregator mockAggregator
        )
        {
            "Given an existing aggregator"
                .x(() =>
                {
                    id = _fixture.Create<Guid>();
                    mockAggregator = new MockAggregator(id);
                });

            "When a property is changed in the aggregator"
                .x(() => mockAggregator.ChangeMockProperty(mockProperty));

            "Then the aggregator is updated with the details of the property"
                .x(() =>
                {
                    mockAggregator.MockProperty.Should().Be(mockProperty);
                    mockAggregator.FlushPendingEvents().FirstOrDefault().Should().BeOfType<MockPropertyChangedEvent>();
                    mockAggregator.Version.Should().Be(1);
                });
        }

        [Scenario]
        public void Load_WhenAnAggregatorHasAnEventThatIsOutOfOrder_ThenAnExceptionShouldBeThrown
        (
            Guid id,
            IAggregateRoot mockAggregator,
            IEvent mockEvent,
            Action action
        )
        {
            "Given an existing aggregator"
                .x(() =>
                {
                    id = _fixture.Create<Guid>();
                    mockAggregator = new MockAggregator(id);
                });

            "And an existing event"
                .x(() => { mockEvent = _fixture.Create<MockPropertyChangedEvent>(); });

            "And the event has an incorrect version"
                .x(() => mockEvent.Version = 100);

            "When the aggregator loads the event"
                .x(() => { action = () => mockAggregator.Load(new List<IEvent> {mockEvent}); });

            "Then an error is thrown indicating the event is out of order"
                .x(() => action.Should().Throw<AggregateEventOutOfOrderException>());
        }

        [Scenario]
        public void Load_WhenAnAggregatorLoadsEventsFromHistory_ThenTheAggregatorShouldUpdateWithTheDetailsOfTheEvents
        (
            Guid id,
            string mockProperty,
            MockAggregator mockAggregator,
            IEvent mockEvent
        )
        {
            "Given an aggregate"
                .x(() => mockAggregator = _fixture.Create<MockAggregator>());

            "And an aggregate Id"
                .x(() => id = _fixture.Create<Guid>());

            "And an event loaded from the event store"
                .x(() =>
                {
                    mockProperty = _fixture.Create<string>();
                    mockEvent = new MockPropertyChangedEvent(id, mockProperty);
                });

            "And the event has the correct version"
                .x(() => mockEvent.Version = 1);

            "When the aggregator loads the event"
                .x(() => mockAggregator.Load(new[] {mockEvent}));

            "Then the aggregator is updated with the details of the event"
                .x(() =>
                {
                    mockAggregator.Id.Should().Be(id);
                    mockAggregator.MockProperty.Should().Be(mockProperty);
                    mockAggregator.Version.Should().Be(mockEvent.Version);
                });
        }

        [Scenario]
        public void FlushPendingEvents_WhenAggregateAndEventHaveNoAggregateId_ThenAnErrorShouldBeThrown
        (
            MockAggregator mockAggregator,
            Action action
        )
        {
            "Give an aggregate with no id"
                .x(() => mockAggregator = new MockAggregator());

            "And a property is changed in the aggregator"
                .x(() => mockAggregator.ChangeMockProperty(null));

            "When the request to flush pending events is called"
                .x(() => action = () => mockAggregator.FlushPendingEvents());

            "Then an exception is thrown"
                .x(() => action.Should().Throw<AggregateEventMissingIdException>());
        }

        [Scenario]
        public void FlushPendingEvents_WhenThePendingEventsAreFlushed_ThenTheAggregatorShouldHaveAnUpdatedVersion
        (
            Guid id,
            string mockProperty,
            MockAggregator mockAggregator,
            IEvent mockEvent
        )
        {
            "Given an aggregator"
                .x(() =>
                {
                    id = _fixture.Create<Guid>();
                    mockAggregator = new MockAggregator(id);
                });

            "And a property value"
                .x(() => { mockProperty = _fixture.Create<string>(); });

            "And the property is applied to the aggregator"
                .x(() => { mockAggregator.ChangeMockProperty(mockProperty); });

            "When the pending events are flushed"
                .x(() => mockEvent = mockAggregator.FlushPendingEvents().First());

            "Then the aggregator version is updated"
                .x(() => mockAggregator.Version.Should().Be(mockEvent.Version));

            "And the command is applied to the aggregator"
                .x(() => mockAggregator.MockProperty.Should().Be(mockProperty));

            "And the event id is the same as the aggregator id"
                .x(() => mockEvent.Id.Should().Be(id));

            "And the event timestamp is set"
                .x(() => mockEvent.TimeStamp.Date.Should().Be(DateTimeOffset.UtcNow.Date));
        }

        [Scenario]
        public void CheckRule_WhenAggregateAndAndValidRule_ThenShouldNotThrowError
        (
            MockAggregator mockAggregator,
            Action action
        )
        {
            "Give an aggregate with no id"
                .x(() => mockAggregator = new MockAggregator());

            "When a rule is validated"
                .x(() => action = () => mockAggregator.CheckBusinessRule(true));

            "Then an exception is not thrown"
                .x(() => action.Should().NotThrow());
        }

        [Scenario]
        public void CheckRule_WhenAggregateAndAndInvalidRule_ThenShouldThrowError
        (
            MockAggregator mockAggregator,
            Action action
        )
        {
            "Give an aggregate with no id"
                .x(() => mockAggregator = new MockAggregator());

            "When a rule is validated"
                .x(() => action = () => mockAggregator.CheckBusinessRule(false));

            "Then an exception is thrown"
                .x(() => action.Should().Throw<RuleValidationException>());
        }
    }
}
