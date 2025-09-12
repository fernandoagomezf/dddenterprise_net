using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class OrganizationUnit
    : AggregateRoot {
    private readonly string _name;
    private readonly Employee _topManager;
    private readonly EmployeeCollection _employees;

    public OrganizationUnit(Employee topManager, string name) {
        ArgumentNullException.ThrowIfNull(topManager);
        ArgumentNullException.ThrowIfNull(name);
        if (topManager.Status == Status.Active) {
            throw new DomainException("Only active employees can be assigned to a organization unit.");
        }
        if (string.IsNullOrWhiteSpace(name)) {
            throw new DomainException("Organization unit must have a valid name.");
        }
        _topManager = topManager;
        _employees = new();
        _name = name;
    }

    public string Name => _name;
    public Employee TopManager => _topManager;
    public EmployeeView Employees => new EmployeeView(_employees);

    public Employee Create(PersonName name, DateTime birthDate) {
        ArgumentNullException.ThrowIfNull(name);
        if (birthDate.AddYears(18) < DateTime.Now) {
            throw new DomainException("Can only create employees that are at least 18 years old.");
        }
        var employee = new Employee(this, name, birthDate);
        employee.DomainEventOccurred += HandleAggregateEvents;
        return employee;
    }
}
