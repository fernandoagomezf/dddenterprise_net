using System;
using VantagePoint.Domain.Organization;
using Xunit;

namespace VantagePoint.Domain.Organization.Tests;

public class EmailTests {
    [Fact]
    public void Constructor_ValidEmail_SetsValue() {
        var email = new Email("user@example.com");
        Assert.Equal("user@example.com", email.Value);
    }

    [Fact]
    public void Constructor_Null_ThrowsArgumentNullException() {
        Assert.Throws<ArgumentNullException>(() => new Email(null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WhiteSpace_ThrowsArgumentException(string value) {
        Assert.Throws<ArgumentException>(() => new Email(value));
    }

    [Theory]
    [InlineData("invalidemail")]
    [InlineData("user@.com")]
    [InlineData("@example.com")]
    [InlineData("user@com")]
    [InlineData("user@domain.")]
    public void Constructor_InvalidFormat_ThrowsArgumentException(string value) {
        Assert.Throws<ArgumentException>(() => new Email(value));
    }

    [Fact]
    public void Empty_StaticProperty_IsEmptyEmail() {
        var empty = Email.Empty;
        Assert.Equal(string.Empty, empty.Value);
    }

    [Fact]
    public void Equality_TwoInstancesWithSameValue_AreEqual() {
        var email1 = new Email("user@example.com");
        var email2 = new Email("user@example.com");
        Assert.Equal(email1, email2);
        Assert.True(email1 == email2);
        Assert.False(email1 != email2);
    }

    [Fact]
    public void Equality_TwoInstancesWithDifferentValue_AreNotEqual() {
        var email1 = new Email("user1@example.com");
        var email2 = new Email("user2@example.com");
        Assert.NotEqual(email1, email2);
        Assert.False(email1 == email2);
        Assert.True(email1 != email2);
    }
}
