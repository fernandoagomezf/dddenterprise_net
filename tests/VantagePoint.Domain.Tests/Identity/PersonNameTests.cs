using System;
using VantagePoint.Domain.Identity;
using Xunit;

namespace VantagePoint.Domain.Identity.Tests;

public class PersonNameTests {
    [Fact]
    public void Constructor_SetsFirstAndLastName() {
        var personName = new PersonName("Fernando Arturo", "Gómez Flores");
        Assert.Equal("Fernando Arturo", personName.FirstName);
        Assert.Equal("Gómez Flores", personName.LastName);
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_ForNullFirstName() {
        Assert.Throws<ArgumentNullException>(() => new PersonName(null!, "Gómez Flores"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(" \t\n  ")]
    public void Constructor_ThrowsArgumentException_ForInvalidFirstName(string invalidFirstName) {
        Assert.Throws<ArgumentException>(() => new PersonName(invalidFirstName, "Gómez Flores"));
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_ForNullLastName() {
        Assert.Throws<ArgumentNullException>(() => new PersonName("Fernando Arturo", null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(" \t\n  ")]
    public void Constructor_ThrowsArgumentException_ForInvalidLastName(string invalidLastName) {
        Assert.Throws<ArgumentException>(() => new PersonName("Fernando Arturo", invalidLastName));
    }

    [Fact]
    public void Equality_TwoInstancesWithSameValues_AreEqual() {
        var name1 = new PersonName("Gabriela", "Gómez");
        var name2 = new PersonName("Gabriela", "Gómez");
        Assert.Equal(name1, name2);
        Assert.True(name1 == name2);
        Assert.False(name1 != name2);
    }

    [Fact]
    public void Equality_TwoInstancesWithDifferentValues_AreNotEqual() {
        var name1 = new PersonName("Jimena", "Gómez");
        var name2 = new PersonName("Ximena", "Pérez");
        Assert.NotEqual(name1, name2);
        Assert.False(name1 == name2);
        Assert.True(name1 != name2);
    }

    [Fact]
    public void WithExpression_CreatesNewInstanceWithModifiedProperty() {
        var original = new PersonName("Juan", "Gómez");
        var modified = original with { FirstName = "Arturo" };
        Assert.Equal("Arturo", modified.FirstName);
        Assert.Equal("Gómez", modified.LastName);
        Assert.NotEqual(original, modified);
    }

    [Fact]
    public void Deconstruct_ReturnsFirstAndLastName() {
        // Arrange
        var person = new PersonName("Fernando Arturo", "Gómez Flores");

        // Act
        var (first, last) = person;

        // Assert
        Assert.Equal(person.FirstName, first);
        Assert.Equal(person.LastName, last);
    }

}

