using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public record Team
    : ValueObject {
    public Employee Manager { get; init; }
    public EmployeeView Reports { get; init; }

    public Team(Employee manager, EmployeeView reports) {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(reports);
        Manager = manager;
        Reports = reports;
    }

    public void Deconstruct(out Employee manager, out EmployeeView reports) {
        manager = Manager;
        reports = Reports;
    }
}