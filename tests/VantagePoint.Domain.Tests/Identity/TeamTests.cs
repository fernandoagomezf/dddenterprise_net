using System;
using VantagePoint.Domain.Identity;
using Xunit;

namespace VantagePoint.Domain.Identity.Tests;

public class TeamTests {
    [Fact]
    public void Constructor_ValidParameters_SetsProperties() {
        var birthDate = DateTime.Now.AddYears(-25);
        var manager = new Employee(new PersonName("Alice", "Smith"), birthDate);
        var member1 = new Employee(new PersonName("Bob", "Jones"), birthDate);
        var member2 = new Employee(new PersonName("Carol", "White"), birthDate);
        var members = new EmployeeCollection(new[] { member1, member2 });
        var team = new Team(manager, members);
        Assert.Equal(manager, team.Manager);
        Assert.Equal(members, team.Members);
    }

    [Fact]
    public void Constructor_NullManager_ThrowsArgumentNullException() {
        var birthDate = DateTime.Now.AddYears(-25);
        var member = new Employee(new PersonName("Bob", "Jones"), birthDate);
        var members = new EmployeeCollection(new[] { member });
        Assert.Throws<ArgumentNullException>(() => new Team(null!, members));
    }

    [Fact]
    public void Constructor_NullMembers_ThrowsArgumentNullException() {
        var birthDate = DateTime.Now.AddYears(-25);
        var manager = new Employee(new PersonName("Alice", "Smith"), birthDate);
        Assert.Throws<ArgumentNullException>(() => new Team(manager, null!));
    }

    [Fact]
    public void Equality_TwoInstancesWithSameValues_AreEqual() {
        var birthDate = DateTime.Now.AddYears(-25);
        var manager = new Employee(new PersonName("Alice", "Smith"), birthDate);
        var member1 = new Employee(new PersonName("Bob", "Jones"), birthDate);
        var members = new EmployeeCollection(new[] { member1 });
        var team1 = new Team(manager, members);
        var team2 = new Team(manager, members);
        Assert.Equal(team1, team2);
        Assert.True(team1 == team2);
        Assert.False(team1 != team2);
    }

    [Fact]
    public void Equality_TwoInstancesWithDifferentValues_AreNotEqual() {
        var birthDate = DateTime.Now.AddYears(-25);
        var manager1 = new Employee(new PersonName("Alice", "Smith"), birthDate);
        var manager2 = new Employee(new PersonName("Eve", "Black"), birthDate);
        var member1 = new Employee(new PersonName("Bob", "Jones"), birthDate);
        var members = new EmployeeCollection(new[] { member1 });
        var team1 = new Team(manager1, members);
        var team2 = new Team(manager2, members);
        Assert.NotEqual(team1, team2);
        Assert.False(team1 == team2);
        Assert.True(team1 != team2);
    }
}
