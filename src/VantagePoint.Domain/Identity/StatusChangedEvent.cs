using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed record StatusChangedEvent
    : DomainEvent {
    public Identifier EmployeeId { get; init; }
    public Status OldStatus { get; init; }
    public Status NewStatus { get; init; }

    public StatusChangedEvent(Identifier id, Status oldStatus, Status newStatus)
        : base("Identity", "Employee.StatusChanged") {
        ArgumentNullException.ThrowIfNull(id);
        EmployeeId = id;
        OldStatus = oldStatus;
        NewStatus = newStatus;
    }

    public void Deconstruct(out Identifier employeeId, out Status oldStatus, out Status newStatus) {
        employeeId = EmployeeId;
        oldStatus = OldStatus;
        newStatus = NewStatus;
    }
}