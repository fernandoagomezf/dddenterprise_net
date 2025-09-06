using System;
using VantagePoint.Domain.Identity;
using Xunit;

namespace VantagePoint.Domain.Identity.Tests;

public class PhoneNumberTests {
    [Fact]
    public void Constructor_SetsProperties() {
        var phone = new PhoneNumber("1234567890", "+52", "101");
        Assert.Equal("1234567890", phone.MainNumber);
        Assert.Equal("+52", phone.CountryCode);
        Assert.Equal("101", phone.Extension);
    }

    [Fact]
    public void Constructor_OneParameter_SetsMainNumberAndDefaultsOthers() {
        var phone = new PhoneNumber("5551234567");
        Assert.Equal("5551234567", phone.MainNumber);
        Assert.Equal(String.Empty, phone.CountryCode);
        Assert.Equal(String.Empty, phone.Extension);
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_ForNullMainNumber() {
        Assert.Throws<ArgumentNullException>(() =>
            new PhoneNumber(null!, "+52", "101")
        );
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ThrowsArgumentException_ForInvalidMainNumber(string invalidMainNumber) {
        Assert.Throws<ArgumentException>(() => new PhoneNumber(invalidMainNumber, "+52", "101"));
    }

    [Fact]
    public void Equality_TwoInstancesWithSameValues_AreEqual() {
        var phone1 = new PhoneNumber("1234567890", "+52", "101");
        var phone2 = new PhoneNumber("1234567890", "+52", "101");
        Assert.Equal(phone1, phone2);
        Assert.True(phone1 == phone2);
        Assert.False(phone1 != phone2);
    }

    [Fact]
    public void Equality_TwoInstancesWithDifferentValues_AreNotEqual() {
        var phone1 = new PhoneNumber("1234567890", "+52", "101");
        var phone2 = new PhoneNumber("0987654321", "+52", "101");
        Assert.NotEqual(phone1, phone2);
        Assert.False(phone1 == phone2);
        Assert.True(phone1 != phone2);
    }

    [Fact]
    public void WithExpression_CreatesNewInstanceWithModifiedProperty() {
        var original = new PhoneNumber("1234567890", "+52", "101");
        var modified = original with { MainNumber = "0987654321" };
        Assert.Equal("0987654321", modified.MainNumber);
        Assert.Equal("+52", modified.CountryCode);
        Assert.Equal("101", modified.Extension);
        Assert.NotEqual(original, modified);
    }
}

