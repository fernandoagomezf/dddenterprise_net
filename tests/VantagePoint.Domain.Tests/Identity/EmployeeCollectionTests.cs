using System;
using System.Collections.Generic;
using System.Linq;
using VantagePoint.Domain.Common;
using VantagePoint.Domain.Identity;
using Xunit;

namespace VantagePoint.Domain.Identity.Tests;

public class EmployeeCollectionTests {
    [Fact]
    public void Constructor_Empty_CreatesEmptyCollection() {
        var collection = new EmployeeCollection();
        Assert.Equal(0, collection.Count);
    }

    [Fact]
    public void Constructor_WithEmployees_AddsAllEmployees() {
        var birthDate = DateTime.Now.AddYears(-30);
        var emp1 = new Employee(new PersonName("Alice", "Smith"), birthDate);
        var emp2 = new Employee(new PersonName("Bob", "Jones"), birthDate);
        var collection = new EmployeeCollection(new[] { emp1, emp2 });
        Assert.Equal(2, collection.Count);
        Assert.Contains(emp1, collection);
        Assert.Contains(emp2, collection);
    }

    [Fact]
    public void Find_ReturnsEmployee_WhenExists() {
        var birthDate = DateTime.Now.AddYears(-30);
        var emp = new Employee(new PersonName("Alice", "Smith"), birthDate);
        var collection = new EmployeeCollection(new[] { emp });
        var found = collection.Find(((IEntity)emp).Id);
        Assert.Equal(emp, found);
    }

    [Fact]
    public void Find_ReturnsNull_WhenNotExists() {
        var collection = new EmployeeCollection();
        var found = collection.Find(Guid.NewGuid());
        Assert.Null(found);
    }

    [Fact]
    public void Get_ReturnsEmployee_WhenExists() {
        var birthDate = DateTime.Now.AddYears(-30);
        var emp = new Employee(new PersonName("Alice", "Smith"), birthDate);
        var collection = new EmployeeCollection(new[] { emp });
        var found = collection.Get(((IEntity)emp).Id);
        Assert.Equal(emp, found);
    }

    [Fact]
    public void Get_ThrowsArgumentException_WhenNotExists() {
        var collection = new EmployeeCollection();
        Assert.Throws<ArgumentException>(() => collection.Get(Guid.NewGuid()));
    }

    [Fact]
    public void Enumerator_EnumeratesAllEmployees() {
        var birthDate = DateTime.Now.AddYears(-30);
        var emp1 = new Employee(new PersonName("Alice", "Smith"), birthDate);
        var emp2 = new Employee(new PersonName("Bob", "Jones"), birthDate);
        var collection = new EmployeeCollection(new[] { emp1, emp2 });
        var employees = ((IEnumerable<Employee>)collection).ToList();
        Assert.Contains(emp1, employees);
        Assert.Contains(emp2, employees);
        Assert.Equal(2, employees.Count);
    }
}
