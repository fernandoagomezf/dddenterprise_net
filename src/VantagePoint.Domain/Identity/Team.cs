using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public record Team
    : ValueObject {
    public Employee Manager { get; init; }
    public EmployeeCollection Members { get; init; }

    public Team(Employee manager, EmployeeCollection members) {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(members);

        Manager = manager;
        Members = members;
    }
}