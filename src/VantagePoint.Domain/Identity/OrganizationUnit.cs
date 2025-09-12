using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed class OrganizationUnit
    : AggregateRoot {
    private readonly string _name;
    private readonly Employee _topManager;
    private readonly EmployeeCollection _employees;
    private readonly EmployeeMapCollection _maps;

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
        _name = name;
        _employees = new();
        _maps = new(this);
    }

    public string Name => _name;
    public Employee TopManager => _topManager;
    public EmployeeView Employees => new EmployeeView(_employees);

    private void EnsureOfAge(DateTime birthDate) {
        if (birthDate > DateTime.Now.AddYears(-18)) {
            throw new DomainException("Employee must be at least 18 years old.");
        }
    }

    public Employee Create(PersonName name, DateTime birthDate) {
        ArgumentNullException.ThrowIfNull(name);
        EnsureOfAge(birthDate);

        var employee = new Employee(this, name, birthDate);
        employee.DomainEventOccurred += HandleAggregateEvents;
        _employees.Add(employee);
        HandleAggregateEvents(new OrganizationChangedEvent(Id));

        return employee;
    }

    public void AssignManager(Employee manager, Employee report) {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(report);
        if (manager.Status != Status.Active || report.Status != Status.Active) {
            throw new DomainException("Both employees must be active to assign a manager.");
        }
        if (manager.Id == report.Id) {
            throw new DomainException("An employee cannot be their own manager.");
        }
        if (manager.OrganizationUnit.Id != Id || report.OrganizationUnit.Id != Id) {
            throw new DomainException("Both employees must belong to the organization unit.");
        }
        if (_maps.ContainsMap(manager, report)) {
            throw new DomainException("The manager-report relationship already exists.");
        }

        var map = _maps.Find(report);
        if (map is not null) {
            _maps.Remove(map);
        }
        _maps.Add(new EmployeeMap(manager, report));

        HandleAggregateEvents(new StructureChangedEvent(Id, manager.Id, report.Id));
    }
}
