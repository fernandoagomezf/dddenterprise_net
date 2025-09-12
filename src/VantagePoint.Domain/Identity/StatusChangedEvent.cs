using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed record StatusChangedEvent
    : DomainEvent {
    public Identifier OrganizationUnit { get; init; }
    public Identifier Employee { get; init; }
    public Status OldStatus { get; init; }
    public Status NewStatus { get; init; }

    public StatusChangedEvent(Identifier organizationUnit, Identifier employee, Status oldStatus, Status newStatus)
        : base("Identity", "Employee.StatusChanged") {
        ArgumentNullException.ThrowIfNull(employee);
        OrganizationUnit = organizationUnit;
        Employee = employee;
        OldStatus = oldStatus;
        NewStatus = newStatus;
    }

    public void Deconstruct(out Identifier organizationUnit, out Identifier employee, out Status oldStatus, out Status newStatus) {
        organizationUnit = OrganizationUnit;
        employee = Employee;
        oldStatus = OldStatus;
        newStatus = NewStatus;
    }
}