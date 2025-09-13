using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace VantagePoint.Domain.Common.Tests;

public class DomainEventTests {
    [Fact]
    public void Constructor_WithAllParameters_ShouldSetProperties() {
        // Arrange
        var context = "UserManagement";
        var code = "UserCreated";
        var raised = new DateTime(2023, 1, 1, 12, 0, 0);

        // Act
        var domainEvent = new DomainEvent(context, code, raised);

        // Assert
        Assert.Equal(context, domainEvent.Context);
        Assert.Equal(code, domainEvent.Code);
        Assert.Equal(raised, domainEvent.Raised);
    }

    [Fact]
    public void Constructor_WithoutRaisedParameter_ShouldSetCurrentTime() {
        // Arrange
        var context = "UserManagement";
        var code = "UserCreated";
        var beforeCreation = DateTime.Now;

        // Act
        var domainEvent = new DomainEvent(context, code);
        var afterCreation = DateTime.Now;

        // Assert
        Assert.Equal(context, domainEvent.Context);
        Assert.Equal(code, domainEvent.Code);
        Assert.InRange(domainEvent.Raised, beforeCreation, afterCreation);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentException() {
        // Arrange
        string context = null!;
        var code = "UserCreated";

        // Act & Assert 
        Assert.Throws<ArgumentNullException>(() => new DomainEvent(context, code));
    }

    [Fact]
    public void Constructor_WithEmptyContext_ShouldThrowArgumentException() {
        // Arrange
        var context = String.Empty;
        var code = "UserCreated";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new DomainEvent(context, code));
    }

    [Fact]
    public void Constructor_WithWhiteSpaceContext_ShouldThrowArgumentException() {
        // Arrange
        var context = "   ";
        var code = "UserCreated";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new DomainEvent(context, code));
    }

    [Fact]
    public void Constructor_WithNullCode_ShouldThrowArgumentException() {
        // Arrange
        var context = "UserManagement";
        string code = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new DomainEvent(context, code));
    }

    [Fact]
    public void Record_Equality_ShouldWorkCorrectly() {
        // Arrange
        var raised = new DateTime(2023, 1, 1, 12, 0, 0);
        var event1 = new DomainEvent("UserManagement", "UserCreated", raised);
        var event2 = new DomainEvent("UserManagement", "UserCreated", raised);

        // Act & Assert
        Assert.Equal(event1, event2);
        Assert.True(event1 == event2);
        Assert.False(event1 != event2);
    }

    [Fact]
    public void Record_Inequality_ShouldWorkCorrectly() {
        // Arrange
        var raised = new DateTime(2023, 1, 1, 12, 0, 0);
        var event1 = new DomainEvent("UserManagement", "UserCreated", raised);
        var event2 = new DomainEvent("OrderManagement", "OrderCreated", raised);

        // Act & Assert
        Assert.NotEqual(event1, event2);
        Assert.False(event1 == event2);
        Assert.True(event1 != event2);
    }

    [Fact]
    public void Record_GetHashCode_ShouldBeConsistent() {
        // Arrange
        var raised = new DateTime(2023, 1, 1, 12, 0, 0);
        var event1 = new DomainEvent("UserManagement", "UserCreated", raised);
        var event2 = new DomainEvent("UserManagement", "UserCreated", raised);

        // Act & Assert
        Assert.Equal(event1.GetHashCode(), event2.GetHashCode());
    }

    [Fact]
    public void Record_ToString_ShouldReturnMeaningfulRepresentation() {
        // Arrange
        var domainEvent = new DomainEvent("UserManagement", "UserCreated", new DateTime(2023, 1, 1, 12, 0, 0));

        // Act
        var result = domainEvent.ToString();

        // Assert   
        Assert.Contains("UserManagement", result);
        Assert.Contains("UserCreated", result);
        Assert.Contains("2023", result);
    }

    [Fact]
    public void Equals_WithTwoDomains_ShouldBeEqualAlways() {
        // Arrange
        var domain1 = new DomainEvent("MyContext", "MyEvent", new DateTime(2025, 9, 5));
        var domain2 = new DomainEvent("MyContext", "MyEvent", new DateTime(2025, 9, 5));

        // Act & Assert
        Assert.False(ReferenceEquals(domain1, domain2));
        Assert.True(domain1 == domain2);
        Assert.True(domain1.Equals(domain2));
    }

    [Fact]
    public void Equals_WithNullDomain_ShouldNotBeEquals() {
        // Arrange
        var domain1 = new DomainEvent("MyContext", "MyEvent", new DateTime(2025, 9, 5));

        // Act & Assert
        Assert.False(domain1 == null);
        Assert.False(domain1.Equals(null));
    }

    [Fact]
    public void Deconstruct_ReturnsAllValues() {
        // Arrange
        var raised = new DateTime(2023, 1, 1, 12, 0, 0);
        var evt = new DomainEvent("UserManagement", "UserCreated", raised);

        // Act
        evt.Deconstruct(out var context, out var code, out var when);

        // Assert
        Assert.Equal("UserManagement", context);
        Assert.Equal("UserCreated", code);
        Assert.Equal(raised, when);
    }

    [Fact]
    public void Constructor_WithEmptyCode_ShouldThrowArgumentException() {
        // Arrange
        var context = "UserManagement";
        var code = String.Empty;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new DomainEvent(context, code));
    }

    [Fact]
    public void Constructor_WithWhiteSpaceCode_ShouldThrowArgumentException() {
        // Arrange
        var context = "UserManagement";
        var code = "   ";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new DomainEvent(context, code));
    }

    [Fact]
    public void Record_WithExpression_ShouldCreateModifiedCopy() {
        // Arrange
        var originalRaised = new DateTime(2023, 1, 1, 12, 0, 0);
        var originalEvent = new DomainEvent("UserManagement", "UserCreated", originalRaised);
        var newRaised = new DateTime(2023, 1, 1, 13, 0, 0);

        // Act
        var modifiedEvent = originalEvent with {
            Code = "UserUpdated",
            Raised = newRaised
        };

        // Assert
        Assert.Equal("UserManagement", modifiedEvent.Context);
        Assert.Equal("UserUpdated", modifiedEvent.Code);
        Assert.Equal(newRaised, modifiedEvent.Raised);

        Assert.Equal("UserManagement", originalEvent.Context);
        Assert.Equal("UserCreated", originalEvent.Code);
        Assert.Equal(originalRaised, originalEvent.Raised);

        Assert.NotSame(originalEvent, modifiedEvent);
        Assert.NotEqual(originalEvent, modifiedEvent);
    }

    [Fact]
    public void Record_WithExpression_SinglePropertyChange_ShouldWork() {
        // Arrange
        var originalEvent = new DomainEvent("UserManagement", "UserCreated", DateTime.Now);

        // Act
        var modifiedEvent = originalEvent with {
            Context = "OrderManagement"
        };

        // Assert
        Assert.Equal("OrderManagement", modifiedEvent.Context);
        Assert.Equal("UserCreated", modifiedEvent.Code);
        Assert.Equal(originalEvent.Raised, modifiedEvent.Raised);
    }
}