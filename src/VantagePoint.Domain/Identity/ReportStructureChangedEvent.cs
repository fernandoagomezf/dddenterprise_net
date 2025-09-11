using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed record ReportStructureChangedEvent
    : DomainEvent {
    public Identifier EmployeeId { get; init; }
    public Identifier ReportId { get; init; }

    public ReportStructureChangedEvent(Identifier id, Identifier reportId)
        : base("Identity", "Employee.ReportStructureChanged") {
        ArgumentNullException.ThrowIfNull(id);
        EmployeeId = id;
        ReportId = reportId;
    }

    public void Deconstruct(out Identifier employeeId, out Identifier reportId) {
        employeeId = EmployeeId;
        reportId = ReportId;
    }
}