using System;
using AutoFixture;
using FluentAssertions;
using MediatR.DDD.Exceptions;
using Moq;
using Xunit;

namespace MediatR.DDD.Tests.Exceptions
{
    public sealed class AggregateEventMissingIdExceptionTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Fact]
        public void Constructor_WhenAggregateTypeAndEventTypeAreSpecified_ShouldThrowExceptionWithDescriptiveMessage()
        {
            Type mockAggregateType = new Mock<IAggregateRoot>().Object.GetType();
            Type mockEventType = new Mock<IEvent>().Object.GetType();

            Action action = () => throw new AggregateEventMissingIdException(mockAggregateType, mockEventType);

            action
                .Should()
                .Throw<AggregateEventMissingIdException>()
                .WithMessage($"Attempted to save event of type {mockEventType.FullName} from {mockAggregateType.FullName} but no IDs where set.");
        }

        [Fact]
        public void Constructor_WhenMessageAndInnerExceptionAreSpecified_ShouldThrowExceptionWithSpecifiedMessageAndInnerException()
        {
            string message = _fixture.Create<string>();
            Exception innerException = _fixture.Create<NotSupportedException>();

            Action action = () => throw new AggregateEventMissingIdException(message, innerException);

            action.Should().Throw<AggregateEventMissingIdException>().WithMessage(message).WithInnerException<NotSupportedException>();
        }

        [Fact]
        public void Constructor_WhenMessageIsSpecified_ShouldThrowExceptionWithSpecifiedMessage()
        {
            string message = _fixture.Create<string>();

            Action action = () => throw new AggregateEventMissingIdException(message);

            action.Should().Throw<AggregateEventMissingIdException>().WithMessage(message);
        }

        [Fact]
        public void Constructor_WhenNoConstructorParametersAreSpecified_ShouldConstructException()
        {
            Action action = () => throw new AggregateEventMissingIdException();

            action.Should().Throw<AggregateEventMissingIdException>();
        }
    }
}