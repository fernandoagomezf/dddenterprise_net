using System;
using Xunit;

namespace VantagePoint.Domain.Common.Tests;

public class DomainExceptionTests {
    [Fact]
    public void Constructor_Default_ShouldSetDefaultMessage() {
        var exception = new DomainException();

        Assert.Equal("A problem ocurred within the domain.", exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void Constructor_WithMessage_ShouldSetProvidedMessage() {
        var expectedMessage = "Custom domain error message";

        var exception = new DomainException(expectedMessage);

        Assert.Equal(expectedMessage, exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetBothProperties() {
        var expectedMessage = "Custom domain error message";
        var innerException = new InvalidOperationException("Inner exception message");

        var exception = new DomainException(expectedMessage, innerException);

        Assert.Equal(expectedMessage, exception.Message);
        Assert.Same(innerException, exception.InnerException);
    }

    [Fact]
    public void Constructor_WithNullMessage_ShouldSetNullMessage() {
        var expectedMessage = "Exception of type 'VantagePoint.Domain.Common.DomainException' was thrown.";

        var exception = new DomainException(null!);

        Assert.Equal(expectedMessage, exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void Constructor_WithEmptyMessage_ShouldSetEmptyMessage() {
        var exception = new DomainException(string.Empty);

        Assert.Equal(string.Empty, exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void Constructor_WithMessageAndNullInnerException_ShouldSetMessageAndNullInnerException() {
        var expectedMessage = "Custom domain error message";

        var exception = new DomainException(expectedMessage, null!);

        Assert.Equal(expectedMessage, exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void Instance_ShouldBeAssignableToException() {
        var exception = new DomainException();

        Assert.IsAssignableFrom<Exception>(exception);
    }

    [Fact]
    public void Instance_ShouldBeOfTypeDomainException() {
        var exception = new DomainException();

        Assert.IsType<DomainException>(exception);
    }
}