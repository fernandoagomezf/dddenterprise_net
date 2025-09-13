using System;
using Xunit;

namespace VantagePoint.Domain.Common.Tests;

public class EntityTests {
    private sealed class TestEntity : Entity {
        public TestEntity(Identifier id)
            : base(id) {
        }
    }

    [Fact]
    public void Ctor_WithValidIdentifier_SetsIdProperty() {
        // Arrange
        var id = Identifier.New();

        // Act
        var entity = new TestEntity(id);

        // Assert
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void Ctor_NullIdentifier_ThrowsArgumentNullException() {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new TestEntity(null!));
    }

    [Fact]
    public void Equals_SameId_ReturnsTrue() {
        // Arrange
        var id = Identifier.New();
        var entity1 = new TestEntity(id);
        var entity2 = new TestEntity(id);

        // Act
        var result = entity1.Equals(entity2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_DifferentId_ReturnsFalse() {
        // Arrange
        var entity1 = new TestEntity(Identifier.New());
        var entity2 = new TestEntity(Identifier.New());

        // Act
        var result = entity1.Equals(entity2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse() {
        // Arrange
        var entity = new TestEntity(Identifier.New());

        // Act
        var result = entity.Equals((Entity?)null!);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_DifferentObject_ReturnsFalse() {
        // Arrange
        var entity = new TestEntity(Identifier.New());
        var other = new object();

        // Act
        var result = entity.Equals(other);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetHashCode_SameId_SameHashCode() {
        // Arrange
        var id = Identifier.New();
        var entity1 = new TestEntity(id);
        var entity2 = new TestEntity(id);

        // Act
        var hash1 = entity1.GetHashCode();
        var hash2 = entity2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }
}
