using System;
using VantagePoint.Domain.Identity;
using Xunit;

namespace VantagePoint.Domain.Identity.Tests;

public class AddressTests {
    [Fact]
    public void Constructor_ValidParameters_SetsProperties() {
        var address = new Address("123 Main St", "Springfield", "IL", "USA", "62704");
        Assert.Equal("123 Main St", address.Street);
        Assert.Equal("Springfield", address.City);
        Assert.Equal("IL", address.State);
        Assert.Equal("USA", address.Country);
        Assert.Equal("62704", address.PostalCode);
    }

    [Theory]
    [InlineData(null)]
    public void Constructor_NullStreet_ThrowsArgumentNullException(string? value) {
        Assert.Throws<ArgumentNullException>(() => new Address(value!, "City", "State", "Country", "12345"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WhiteSpaceStreet_ThrowsArgumentException(string value) {
        Assert.Throws<ArgumentException>(() => new Address(value, "City", "State", "Country", "12345"));
    }

    [Theory]
    [InlineData(null)]
    public void Constructor_NullCity_ThrowsArgumentNullException(string? value) {
        Assert.Throws<ArgumentNullException>(() => new Address("Street", value!, "State", "Country", "12345"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WhiteSpaceCity_ThrowsArgumentException(string value) {
        Assert.Throws<ArgumentException>(() => new Address("Street", value, "State", "Country", "12345"));
    }

    [Theory]
    [InlineData(null)]
    public void Constructor_NullState_ThrowsArgumentNullException(string? value) {
        Assert.Throws<ArgumentNullException>(() => new Address("Street", "City", value!, "Country", "12345"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WhiteSpaceState_ThrowsArgumentException(string value) {
        Assert.Throws<ArgumentException>(() => new Address("Street", "City", value, "Country", "12345"));
    }

    [Theory]
    [InlineData(null)]
    public void Constructor_NullPostalCode_ThrowsArgumentNullException(string? value) {
        Assert.Throws<ArgumentNullException>(() => new Address("Street", "City", "State", "Country", value!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WhiteSpacePostalCode_ThrowsArgumentException(string value) {
        Assert.Throws<ArgumentException>(() => new Address("Street", "City", "State", "Country", value));
    }

    [Fact]
    public void Empty_StaticProperty_IsEmptyAddress() {
        var empty = Address.Empty;
        Assert.Equal(string.Empty, empty.Street);
        Assert.Equal(string.Empty, empty.City);
        Assert.Equal(string.Empty, empty.State);
        Assert.Equal(string.Empty, empty.Country);
        Assert.Equal(string.Empty, empty.PostalCode);
    }

    [Fact]
    public void Equality_TwoInstancesWithSameValues_AreEqual() {
        var addr1 = new Address("123 Main St", "Springfield", "IL", "USA", "62704");
        var addr2 = new Address("123 Main St", "Springfield", "IL", "USA", "62704");
        Assert.Equal(addr1, addr2);
        Assert.True(addr1 == addr2);
        Assert.False(addr1 != addr2);
    }

    [Fact]
    public void Equality_TwoInstancesWithDifferentValues_AreNotEqual() {
        var addr1 = new Address("123 Main St", "Springfield", "IL", "USA", "62704");
        var addr2 = new Address("456 Elm St", "Springfield", "IL", "USA", "62704");
        Assert.NotEqual(addr1, addr2);
        Assert.False(addr1 == addr2);
        Assert.True(addr1 != addr2);
    }
}
