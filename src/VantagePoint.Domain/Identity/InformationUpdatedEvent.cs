using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed record InformationUpdatedEvent
    : DomainEvent {
    public Identifier EmployeeId { get; init; }

    public InformationUpdatedEvent(Identifier id)
        : base("Identity", "Employee.InformationUpdated") {
        ArgumentNullException.ThrowIfNull(id);
        EmployeeId = id;
    }

    public void Deconstruct(out Identifier employeeId) {
        employeeId = EmployeeId;
    }
}