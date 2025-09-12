using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Organization;

public record EmployeeMap
    : ValueObject {
    public Identifier OrganizationUnit { get; init; }
    public Identifier Manager { get; init; }
    public Identifier Report { get; init; }

    public EmployeeMap(Employee manager, Employee report) {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(report);
        if (manager.OrganizationUnit.Id != report.OrganizationUnit.Id) {
            throw new DomainException("Both employees must belong to the same organization unit.");
        }
        OrganizationUnit = manager.OrganizationUnit.Id;
        Manager = manager.Id;
        Report = report.Id;
    }

    public void Deconstruct(out Identifier organizationUnit, out Identifier manager, out Identifier report) {
        organizationUnit = OrganizationUnit;
        manager = Manager;
        report = Report;
    }
}