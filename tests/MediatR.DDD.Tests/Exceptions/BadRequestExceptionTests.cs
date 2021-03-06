﻿using System;
using AutoFixture;
using FluentAssertions;
using MediatR.DDD.Exceptions;
using Xunit;

namespace MediatR.DDD.Tests.Exceptions
{
    public sealed class BadRequestExceptionTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Fact]
        public void Constructor_WhenMessageAndInnerExceptionAreSpecified_ShouldThrowExceptionWithSpecifiedMessageAndInnerException()
        {
            string message = _fixture.Create<string>();
            Exception innerException = _fixture.Create<NotSupportedException>();

            Action action = () => throw new BadRequestException(message, innerException);

            action.Should().Throw<BadRequestException>().WithMessage(message).WithInnerException<NotSupportedException>();
        }

        [Fact]
        public void Constructor_WhenMessageIsSpecified_ShouldThrowExceptionWithSpecifiedMessage()
        {
            string message = _fixture.Create<string>();

            Action action = () => throw new BadRequestException(message);

            action.Should().Throw<BadRequestException>().WithMessage(message);
        }

        [Fact]
        public void Constructor_WhenNoConstructorParametersAreSpecified_ShouldConstructException()
        {
            Action action = () => throw new BadRequestException();

            action.Should().Throw<BadRequestException>();
        }
    }
}