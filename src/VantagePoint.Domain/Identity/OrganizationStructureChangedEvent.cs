using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed record OrganizationStructureChangedEvent
    : DomainEvent {
    public Identifier OrganizationUnit { get; init; }
    public Identifier Employee { get; init; }
    public Identifier Report { get; init; }

    public OrganizationStructureChangedEvent(Identifier organizationUnit, Identifier employee, Identifier report)
        : base("Identity", "Employee.OrganizationStructureChanged") {
        ArgumentNullException.ThrowIfNull(organizationUnit);
        ArgumentNullException.ThrowIfNull(employee);
        ArgumentNullException.ThrowIfNull(report);
        OrganizationUnit = organizationUnit;
        Employee = employee;
        Report = report;
    }

    public void Deconstruct(out Identifier organizationUnit, out Identifier employee, out Identifier report) {
        organizationUnit = OrganizationUnit;
        employee = Employee;
        report = Report;
    }
}