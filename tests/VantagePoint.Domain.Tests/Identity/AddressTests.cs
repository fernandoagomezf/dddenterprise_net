using System;
using VantagePoint.Domain.Organization;
using Xunit;

namespace VantagePoint.Domain.Organization.Tests;

public class AddressTests {
    [Fact]
    public void Constructor_WithValidValues_SetsProperties() {
        // Arrange
        var street = "123 Main St";
        var city = "Metropolis";
        var state = "State";
        var postalCode = "12345";
        var country = "Country";

        // Act
        var address = new Address(
            street,
            city,
            state,
            postalCode,
            country
        );

        // Assert
        Assert.Equal(street, address.Street);
        Assert.Equal(city, address.City);
        Assert.Equal(state, address.State);
        Assert.Equal(postalCode, address.PostalCode);
        Assert.Equal(country, address.Country);
    }

    [Fact]
    public void Equality_TwoAddressesWithSameValues_AreEqual() {
        // Arrange
        var a1 = new Address(
            "123 Main St",
            "Metropolis",
            "State",
            "12345",
            "Country"
        );
        var a2 = new Address(
            "123 Main St",
            "Metropolis",
            "State",
            "12345",
            "Country"
        );

        // Act & Assert
        Assert.Equal(a1, a2);
        Assert.True(a1 == a2 || a1.Equals(a2));
    }

    [Fact]
    public void WithExpression_ProducesNewInstance_WithModifiedValue() {
        // Arrange
        var original = new Address(
            "123 Main St",
            "Metropolis",
            "State",
            "12345",
            "Country"
        );

        // Act
        var modified = original with { PostalCode = "99999" };

        // Assert
        Assert.NotSame(original, modified);
        Assert.Equal("99999", modified.PostalCode);
        Assert.Equal("12345", original.PostalCode);

        Assert.Equal(original.Street, modified.Street);
        Assert.Equal(original.City, modified.City);
        Assert.Equal(original.State, modified.State);
        Assert.Equal(original.Country, modified.Country);
    }

    [Fact]
    public void Deconstruct_ReturnsComponentValues() {
        // Arrange
        var address = new Address(
            "123 Main St",
            "Metropolis",
            "State",
            "12345",
            "Country"
        );

        // Act
        var (street, city, state, postalCode, country) = address;

        // Assert
        Assert.Equal(address.Street, street);
        Assert.Equal(address.City, city);
        Assert.Equal(address.State, state);
        Assert.Equal(address.PostalCode, postalCode);
        Assert.Equal(address.Country, country);
    }

}
