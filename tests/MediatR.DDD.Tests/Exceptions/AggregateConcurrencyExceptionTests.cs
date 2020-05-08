using System;
using AutoFixture;
using FluentAssertions;
using MediatR.DDD.Exceptions;
using Xunit;

namespace MediatR.DDD.Tests.Exceptions
{
    public sealed class AggregateConcurrencyExceptionTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Fact]
        public void Constructor_WhenAggregateIdIsSpecified_ShouldThrowExceptionWithAggregateId()
        {
            Guid id = _fixture.Create<Guid>();

            Action action = () => throw new AggregateConcurrencyException(id);

            action
                .Should()
                .Throw<AggregateConcurrencyException>()
                .WithMessage($"Aggregate ID: {id} - Data has been changed between loading and state changes.");
        }

        [Fact]
        public void Constructor_WhenMessageAndInnerExceptionAreSpecified_ShouldThrowExceptionWithSpecifiedMessageAndInnerException()
        {
            string message = _fixture.Create<string>();
            Exception innerException = _fixture.Create<NotSupportedException>();

            Action action = () => throw new AggregateConcurrencyException(message, innerException);

            action.Should().Throw<AggregateConcurrencyException>().WithMessage(message).WithInnerException<NotSupportedException>();
        }

        [Fact]
        public void Constructor_WhenMessageIsSpecified_ShouldThrowExceptionWithSpecifiedMessage()
        {
            string message = _fixture.Create<string>();

            Action action = () => throw new AggregateConcurrencyException(message);

            action.Should().Throw<AggregateConcurrencyException>().WithMessage(message);
        }

        [Fact]
        public void Constructor_WhenNoConstructorParametersAreSpecified_ShouldConstructException()
        {
            Action action = () => throw new AggregateConcurrencyException();

            action.Should().Throw<AggregateConcurrencyException>();
        }
    }
}