using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace VantagePoint.Domain.Common.Tests;

public class DomainEventTests {
    [Fact]
    public void Constructor_WithAllParameters_ShouldSetProperties() {
        var context = "UserManagement";
        var code = "UserCreated";
        var raised = new DateTime(2023, 1, 1, 12, 0, 0);

        var domainEvent = new DomainEvent(context, code, raised);

        Assert.Equal(context, domainEvent.Context);
        Assert.Equal(code, domainEvent.Code);
        Assert.Equal(raised, domainEvent.Raised);
    }

    [Fact]
    public void Constructor_WithoutRaisedParameter_ShouldSetCurrentTime() {
        var context = "UserManagement";
        var code = "UserCreated";
        var beforeCreation = DateTime.Now;

        var domainEvent = new DomainEvent(context, code);
        var afterCreation = DateTime.Now;

        Assert.Equal(context, domainEvent.Context);
        Assert.Equal(code, domainEvent.Code);
        Assert.InRange(domainEvent.Raised, beforeCreation, afterCreation);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentException() {
        string context = null!;
        var code = "UserCreated";

        Assert.Throws<ArgumentNullException>(() => new DomainEvent(context, code));
    }

    [Fact]
    public void Constructor_WithEmptyContext_ShouldThrowArgumentException() {
        var context = String.Empty;
        var code = "UserCreated";

        Assert.Throws<ArgumentException>(() => new DomainEvent(context, code));
    }

    [Fact]
    public void Constructor_WithWhiteSpaceContext_ShouldThrowArgumentException() {
        var context = "   ";
        var code = "UserCreated";

        Assert.Throws<ArgumentException>(() => new DomainEvent(context, code));
    }

    [Fact]
    public void Constructor_WithNullCode_ShouldThrowArgumentException() {
        var context = "UserManagement";
        string code = null!;

        Assert.Throws<ArgumentNullException>(() => new DomainEvent(context, code));
    }

    [Fact]
    public void Record_Equality_ShouldWorkCorrectly() {
        var raised = new DateTime(2023, 1, 1, 12, 0, 0);
        var event1 = new DomainEvent("UserManagement", "UserCreated", raised);
        var event2 = new DomainEvent("UserManagement", "UserCreated", raised);

        Assert.Equal(event1, event2);
        Assert.True(event1 == event2);
        Assert.False(event1 != event2);
    }

    [Fact]
    public void Record_Inequality_ShouldWorkCorrectly() {
        var raised = new DateTime(2023, 1, 1, 12, 0, 0);
        var event1 = new DomainEvent("UserManagement", "UserCreated", raised);
        var event2 = new DomainEvent("OrderManagement", "OrderCreated", raised);

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
        var domainEvent = new DomainEvent("UserManagement", "UserCreated", new DateTime(2023, 1, 1, 12, 0, 0));

        var result = domainEvent.ToString();

        Assert.Contains("UserManagement", result);
        Assert.Contains("UserCreated", result);
        Assert.Contains("2023", result);
    }

    [Fact]
    public void Equals_WithTwoDomains_ShouldBeEqualAlways() {
        var domain1 = new DomainEvent("MyContext", "MyEvent", new DateTime(2025, 9, 5));
        var domain2 = new DomainEvent("MyContext", "MyEvent", new DateTime(2025, 9, 5));

        Assert.False(ReferenceEquals(domain1, domain2));
        Assert.True(domain1 == domain2);
        Assert.True(domain1.Equals(domain2));
    }

    [Fact]
    public void Equals_WithNullDomain_ShouldNotBeEquals() {
        var domain1 = new DomainEvent("MyContext", "MyEvent", new DateTime(2025, 9, 5));

        Assert.False(domain1 == null);
        Assert.False(domain1.Equals(null));
    }

    [Fact]
    public void Record_WithExpression_ShouldCreateModifiedCopy() {
        var originalRaised = new DateTime(2023, 1, 1, 12, 0, 0);
        var originalEvent = new DomainEvent("UserManagement", "UserCreated", originalRaised);
        var newRaised = new DateTime(2023, 1, 1, 13, 0, 0);

        var modifiedEvent = originalEvent with {
            Code = "UserUpdated",
            Raised = newRaised
        };

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
        var originalEvent = new DomainEvent("UserManagement", "UserCreated", DateTime.Now);

        var modifiedEvent = originalEvent with {
            Context = "OrderManagement"
        };

        Assert.Equal("OrderManagement", modifiedEvent.Context);
        Assert.Equal("UserCreated", modifiedEvent.Code);
        Assert.Equal(originalEvent.Raised, modifiedEvent.Raised);
    }
}