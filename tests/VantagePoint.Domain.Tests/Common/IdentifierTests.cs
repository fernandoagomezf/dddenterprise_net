using System;
using Xunit;

namespace VantagePoint.Domain.Common.Tests;

public class IdentifierTests {
    [Fact]
    public void Ctor_WithValidGuid_SetsValueProperty() {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var identifier = new Identifier(guid);

        // Assert
        Assert.Equal(guid, identifier.Value);
    }

    [Fact]
    public void Ctor_EmptyGuid_ThrowsArgumentException() {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Identifier(emptyGuid));
        Assert.Equal("Identifier value cannot be empty. (Parameter 'value')", exception.Message);
    }

    [Fact]
    public void Equals_SameValue_ReturnsTrue() {
        // Arrange
        var guid = Guid.NewGuid();
        var identifier1 = new Identifier(guid);
        var identifier2 = new Identifier(guid);

        // Act
        var result = identifier1.Equals(identifier2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_DifferentValue_ReturnsFalse() {
        // Arrange
        var identifier1 = new Identifier(Guid.NewGuid());
        var identifier2 = new Identifier(Guid.NewGuid());

        // Act
        var result = identifier1.Equals(identifier2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToString_ReturnsGuidString() {
        // Arrange
        var guid = Guid.NewGuid();
        var identifier = new Identifier(guid);

        // Act
        var result = identifier.ToString();

        // Assert
        Assert.Equal(guid.ToString(), result);
    }

    [Fact]
    public void New_ReturnsIdentifierWithNewGuid() {
        // Act
        var identifier1 = Identifier.New();
        var identifier2 = Identifier.New();

        // Assert
        Assert.NotEqual(identifier1, identifier2);
        Assert.NotEqual(identifier1.Value, identifier2.Value);
    }

    [Fact]
    public void OperatorEquality_AndInequality_WorkAsExpected() {
        // Arrange
        var guid = Guid.NewGuid();
        var id1 = new Identifier(guid);
        var id2 = new Identifier(guid);
        var id3 = new Identifier(Guid.NewGuid());

        // Act & Assert
        Assert.True(id1 == id2);
        Assert.False(id1 != id2);
        Assert.False(id1 == id3);
        Assert.True(id1 != id3);
    }

    [Fact]
    public void GetHashCode_SameValue_AreEqual() {
        // Arrange
        var guid = Guid.NewGuid();
        var id1 = new Identifier(guid);
        var id2 = new Identifier(guid);

        // Act
        var h1 = id1.GetHashCode();
        var h2 = id2.GetHashCode();

        // Assert
        Assert.Equal(h1, h2);
    }

    [Fact]
    public void WithExpression_CreatesEqualCopy() {
        // Arrange
        var guid = Guid.NewGuid();
        var id = new Identifier(guid);

        // Act
        var copy = id with { };

        // Assert
        Assert.Equal(id, copy);
    }
}