using System;
using Xunit;

namespace VantagePoint.Domain.Common.Tests;

public class DomainExceptionTests {
    [Fact]
    public void Constructor_Default_ShouldSetDefaultMessage() {
        // Arrange
        var exception = new DomainException();

        // Act & Assert
        Assert.Equal("A problem ocurred within the domain.", exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void Constructor_WithMessage_ShouldSetProvidedMessage() {
        // Arrange
        var expectedMessage = "Custom domain error message";
        var exception = new DomainException(expectedMessage);

        // Act & Assert 
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetBothProperties() {
        // Arrange
        var expectedMessage = "Custom domain error message";
        var innerException = new InvalidOperationException("Inner exception message");
        var exception = new DomainException(expectedMessage, innerException);

        // Act & Assert 
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Same(innerException, exception.InnerException);
    }

    [Fact]
    public void Constructor_WithNullMessage_ShouldSetNullMessage() {
        // Arrange
        var expectedMessage = "Exception of type 'VantagePoint.Domain.Common.DomainException' was thrown.";
        var exception = new DomainException(null!);

        // Act & Assert
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void Constructor_WithEmptyMessage_ShouldSetEmptyMessage() {
        // Arrange
        var exception = new DomainException(string.Empty);

        // Act & Assert
        Assert.Equal(string.Empty, exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void Constructor_WithMessageAndNullInnerException_ShouldSetMessageAndNullInnerException() {
        // Arrange
        var expectedMessage = "Custom domain error message";
        var exception = new DomainException(expectedMessage, null!);

        // Act & Assert
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void Instance_ShouldBeAssignableToException() {
        // Arrange
        var exception = new DomainException();

        // Act & Assert
        Assert.IsAssignableFrom<Exception>(exception);
    }

    [Fact]
    public void Instance_ShouldBeOfTypeDomainException() {
        // Arrange
        var exception = new DomainException();

        // Act & Assert
        Assert.IsType<DomainException>(exception);
    }
}