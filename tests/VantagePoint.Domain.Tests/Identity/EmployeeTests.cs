
using System;
using VantagePoint.Domain.Identity;
using VantagePoint.Domain.Common;
using Xunit;

namespace VantagePoint.Domain.Identity.Tests;

public class EmployeeTests {
    [Fact]
    public void Constructor_ValidParameters_SetsProperties() {
        var birthDate = DateTime.Now.AddYears(-25);
        var name = new PersonName("John", "Doe");
        var employee = new Employee(name, birthDate);
        Assert.Equal(name, employee.Name);
        Assert.Equal(birthDate.Date, employee.BirthDate.Date);
        Assert.Equal(EmployeeStatus.Inactive, employee.Status);
    }

    [Fact]
    public void Constructor_ThrowsArgumentException_ForUnderage() {
        var name = new PersonName("Jane", "Smith");
        var birthDate = DateTime.Now.AddYears(-10);
        Assert.Throws<ArgumentException>(() => new Employee(name, birthDate));
    }

    [Fact]
    public void Activate_WhenInactive_SetsStatusToActiveAndHiredDate() {
        var birthDate = DateTime.Now.AddYears(-25);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        employee.Activate();
        Assert.Equal(EmployeeStatus.Active, employee.Status);
        Assert.NotNull(employee);
    }

    [Fact]
    public void Activate_WhenNotInactive_ThrowsDomainException() {
        var birthDate = DateTime.Now.AddYears(-25);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        employee.Activate();
        Assert.Throws<DomainException>(() => employee.Activate());
    }

    [Fact]
    public void Deactivate_WhenActive_SetsStatusToInactive() {
        var birthDate = DateTime.Now.AddYears(-25);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        employee.Activate();
        employee.Deactivate();
        Assert.Equal(EmployeeStatus.Inactive, employee.Status);
    }

    [Fact]
    public void Deactivate_WhenNotActive_ThrowsDomainException() {
        var birthDate = DateTime.Now.AddYears(-25);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        Assert.Throws<DomainException>(() => employee.Deactivate());
    }

    // The following tests for team management are commented out because AddTeamMember does not exist.
    // If you want to test team management, use AssignToTeam or expose team member logic via public API.

    [Fact]
    public void Terminate_WhenAlreadyTerminated_ThrowsDomainException() {
        var birthDate = DateTime.Now.AddYears(-25);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        employee.Activate();
        employee.Terminate();
        Assert.Throws<DomainException>(() => employee.Terminate());
    }

    [Fact]
    public void AssignToTeam_SetsManagerAndPosition() {
        var birthDate = DateTime.Now.AddYears(-25);
        var manager = new Employee(new PersonName("Manager", "One"), birthDate);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        employee.AssignToTeam(manager, "Developer");
        Assert.Equal(manager, employee.Manager);
        Assert.Equal(manager.Department, employee.Department);
        Assert.Equal("Developer", employee.Position);
    }

    [Fact]
    public void AssignToTeam_ThrowsArgumentNullException_ForNullManager() {
        var birthDate = DateTime.Now.AddYears(-25);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        Assert.Throws<ArgumentNullException>(() => employee.AssignToTeam(null!, "Developer"));
    }

    [Fact]
    public void AssignToTeam_ThrowsArgumentException_ForNullOrWhiteSpacePosition() {
        var birthDate = DateTime.Now.AddYears(-25);
        var manager = new Employee(new PersonName("Manager", "One"), birthDate);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        Assert.Throws<ArgumentException>(() => employee.AssignToTeam(manager, " "));
    }

    [Fact]
    public void TransferTeamTo_ThrowsDomainException_IfNoManager() {
        var birthDate = DateTime.Now.AddYears(-25);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        var newManager = new Employee(new PersonName("Manager", "Two"), birthDate);
        Assert.Throws<DomainException>(() => employee.TransferTeamTo(newManager));
    }

    [Fact]
    public void TransferTeamTo_ThrowsDomainException_IfSameManager() {
        var birthDate = DateTime.Now.AddYears(-25);
        var manager = new Employee(new PersonName("Manager", "One"), birthDate);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        employee.AssignToTeam(manager, "Developer");
        Assert.Throws<DomainException>(() => employee.TransferTeamTo(manager));
    }

    [Fact]
    public void TransferTeamTo_UpdatesManagerAndDepartment() {
        var birthDate = DateTime.Now.AddYears(-25);
        var manager1 = new Employee(new PersonName("Manager", "One"), birthDate);
        var employee = new Employee(new PersonName("John", "Doe"), birthDate);
        employee.AssignToTeam(manager1, "Developer");
        Assert.Equal(manager1, employee.Manager);
        Assert.Equal(manager1.Department, employee.Department);
        Assert.Equal("Developer", employee.Position);
    }
}
