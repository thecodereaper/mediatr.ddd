﻿using System;
using AutoFixture;
using FluentAssertions;
using MediatR.DDD.Exceptions;
using Xunit;

namespace MediatR.DDD.Tests.Exceptions
{
    public sealed class AggregateNotFoundExceptionTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Fact]
        public void Constructor_WhenAggregateIdIsSpecified_ShouldThrowExceptionWithAggregateId()
        {
            Guid id = _fixture.Create<Guid>();

            Action action = () => throw new AggregateNotFoundException(id);

            action
                .Should().Throw<AggregateNotFoundException>()
                .WithMessage($"Aggregate ID: {id} - Not found.");
        }

        [Fact]
        public void Constructor_WhenMessageAndInnerExceptionAreSpecified_ShouldThrowExceptionWithSpecifiedMessageAndInnerException()
        {
            string message = _fixture.Create<string>();
            Exception innerException = _fixture.Create<NotSupportedException>();

            Action action = () => throw new AggregateNotFoundException(message, innerException);

            action.Should().Throw<AggregateNotFoundException>().WithMessage(message).WithInnerException<NotSupportedException>();
        }

        [Fact]
        public void Constructor_WhenMessageIsSpecified_ShouldThrowExceptionWithSpecifiedMessage()
        {
            string message = _fixture.Create<string>();

            Action action = () => throw new AggregateNotFoundException(message);

            action.Should().Throw<AggregateNotFoundException>().WithMessage(message);
        }

        [Fact]
        public void Constructor_WhenNoConstructorParametersAreSpecified_ShouldConstructException()
        {
            Action action = () => throw new AggregateNotFoundException();

            action.Should().Throw<AggregateNotFoundException>();
        }
    }
}