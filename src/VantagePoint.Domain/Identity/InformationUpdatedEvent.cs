using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed record InformationUpdatedEvent
    : DomainEvent {
    public Identifier OrganizationUnit { get; init; }
    public Identifier Employee { get; init; }

    public InformationUpdatedEvent(Identifier organizationUnit, Identifier employeeId)
        : base("Identity", "Employee.InformationUpdated") {
        ArgumentNullException.ThrowIfNull(organizationUnit);
        ArgumentNullException.ThrowIfNull(employeeId);
        OrganizationUnit = organizationUnit;
        Employee = employeeId;
    }

    public void Deconstruct(out Identifier organizationUnit, out Identifier employee) {
        organizationUnit = OrganizationUnit;
        employee = Employee;
    }
}