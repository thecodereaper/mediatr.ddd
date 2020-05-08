using System;
using AutoFixture;
using FluentAssertions;
using MediatR.DDD.Exceptions;
using Xunit;

namespace MediatR.DDD.Tests.Exceptions
{
    public sealed class AggregateEventOutOfOrderExceptionTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Fact]
        public void Constructor_WhenAggregateIdIsSpecified_ShouldThrowExceptionWithAggregateId()
        {
            Guid id = _fixture.Create<Guid>();
            ulong version = _fixture.Create<ulong>();

            Action action = () => throw new AggregateEventOutOfOrderException(id, version);

            action
                .Should()
                .Throw<AggregateEventOutOfOrderException>()
                .WithMessage($"Aggregate ID: {id} - Event with version {version} is out of order.");
        }

        [Fact]
        public void Constructor_WhenMessageAndInnerExceptionAreSpecified_ShouldThrowExceptionWithSpecifiedMessageAndInnerException()
        {
            string message = _fixture.Create<string>();
            Exception innerException = _fixture.Create<NotSupportedException>();

            Action action = () => throw new AggregateEventOutOfOrderException(message, innerException);

            action.Should().Throw<AggregateEventOutOfOrderException>().WithMessage(message).WithInnerException<NotSupportedException>();
        }

        [Fact]
        public void Constructor_WhenMessageIsSpecified_ShouldThrowExceptionWithSpecifiedMessage()
        {
            string message = _fixture.Create<string>();

            Action action = () => throw new AggregateEventOutOfOrderException(message);

            action.Should().Throw<AggregateEventOutOfOrderException>().WithMessage(message);
        }

        [Fact]
        public void Constructor_WhenNoConstructorParametersAreSpecified_ShouldConstructException()
        {
            Action action = () => throw new AggregateEventOutOfOrderException();

            action.Should().Throw<AggregateEventOutOfOrderException>();
        }
    }
}