using System;
using Xunit;

namespace VantagePoint.Domain.Common.Tests;

public class ValueObjectTests {
    [Fact]
    public void ValueObject_Equality_Works() {
        // Arrange
        var vo1 = new TestValueObject("value1");
        var vo2 = new TestValueObject("value1");
        var vo3 = new TestValueObject("value2");

        // Act & Assert
        Assert.Equal(vo1, vo2);
        Assert.NotEqual(vo1, vo3);
        Assert.True(vo1 == vo2);
        Assert.True(vo1 != vo3);
    }

    private record TestValueObject(string Value) : ValueObject { }
}