using System;
using System.Linq;
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

    private void EnsureInOrganizationUnit(Employee employee) {
        if (employee.OrganizationUnit.Id != Id) {
            throw new DomainException("Employee must belong to the organization unit.");
        }
    }

    private void EnsureActive(Employee employee) {
        if (employee.Status != Status.Active) {
            throw new DomainException("Employee must be active to perform this operation.");
        }
    }

    public Team GetTeamFor(Employee manager) {
        ArgumentNullException.ThrowIfNull(manager);
        EnsureInOrganizationUnit(manager);
        EnsureActive(manager);
        var reports = _maps.Where(x => x.Manager == manager.Id);
        var reportEmployees = new EmployeeCollection();
        foreach (var map in reports) {
            var report = _employees.Find(map.Report);
            if (report is not null && report.Status == Status.Active) {
                reportEmployees.Add(report);
            }
        }
        var team = new Team(manager, new EmployeeView(reportEmployees));
        return team;
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

    private void EnsureNotOwnManager(Employee manager, Employee report) {
        if (manager.Id == report.Id) {
            throw new DomainException("An employee cannot be their own manager.");
        }
    }

    private void EnsureMapNotExists(Employee manager, Employee report) {
        if (_maps.ContainsMap(manager, report)) {
            throw new DomainException("The manager-report relationship already exists.");
        }
    }

    private void EnsureMapNotCircular(Employee manager, Employee report) {
        var currentManager = manager;
        while (true) {
            var map = _maps.Find(currentManager);
            if (map is null) {
                break;
            }
            if (map.Manager == report.Id) {
                throw new DomainException("Circular management relationship detected.");
            }
            var nextManager = _employees.Find(map.Manager);
            if (nextManager is null) {
                break;
            }
            currentManager = nextManager;
        }
    }

    public void AssignManager(Employee manager, Employee report) {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(report);
        EnsureInOrganizationUnit(manager);
        EnsureInOrganizationUnit(report);
        EnsureActive(manager);
        EnsureActive(report);
        EnsureNotOwnManager(manager, report);
        EnsureMapNotExists(manager, report);
        EnsureMapNotCircular(manager, report);

        var map = _maps.Find(report);
        if (map is not null) {
            _maps.Remove(map);
        }
        _maps.Add(new EmployeeMap(manager, report));

        HandleAggregateEvents(new StructureChangedEvent(Id, manager.Id, report.Id));
    }
}
