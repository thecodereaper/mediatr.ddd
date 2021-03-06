﻿using System;
using AutoFixture;
using FluentAssertions;
using MediatR.DDD.Exceptions;
using Xunit;

namespace MediatR.DDD.Tests.Exceptions
{
    public sealed class UnauthorizedExceptionTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Fact]
        public void Constructor_WhenMessageAndInnerExceptionAreSpecified_ShouldThrowExceptionWithSpecifiedMessageAndInnerException()
        {
            string message = _fixture.Create<string>();
            Exception innerException = _fixture.Create<NotSupportedException>();

            Action action = () => throw new UnauthorizedException(message, innerException);

            action.Should().Throw<UnauthorizedException>().WithMessage(message).WithInnerException<NotSupportedException>();
        }

        [Fact]
        public void Constructor_WhenMessageIsSpecified_ShouldThrowExceptionWithSpecifiedMessage()
        {
            string message = _fixture.Create<string>();

            Action action = () => throw new UnauthorizedException(message);

            action.Should().Throw<UnauthorizedException>().WithMessage(message);
        }

        [Fact]
        public void Constructor_WhenNoConstructorParametersAreSpecified_ShouldConstructException()
        {
            Action action = () => throw new UnauthorizedException();

            action.Should().Throw<UnauthorizedException>();
        }
    }
}