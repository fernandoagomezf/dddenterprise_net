using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class OrganizationUnit
    : AggregateRoot {
    private readonly string _name;
    private readonly Employee _manager;
    private readonly EmployeeCollection _employees;

    public OrganizationUnit(Employee manager) {
        ArgumentNullException.ThrowIfNull(manager);
        if (manager.Status == Status.Active) {
            throw new DomainException("Only active emplyoees can be assigne to a organization unit.");
        }
        _manager = manager;
    }

    public Employee Manager => _manager;

    public Employees Employees => new EmployeeCollection(_employees);

    public Employee Create(PersonName name, DateTime birthDate) {
        ArgumentNullException.ThrowIfNull(name);
        if (birthDate.AddYears(18) < DateTime.Now) {
            throw new DomainException("Can only create employees that are at least 18 years old.")
        }
        var empoyee = new Employee(name, birthDate);
        Enroll(employee);
        return employee;
    }

    public void Enroll(Employee employee) {

    }

    public void 
}
