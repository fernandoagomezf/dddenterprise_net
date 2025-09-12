using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Organization;

public sealed class PreventTransferIfHasDirectReportsPolicy
    : IEmployeeTransferPolicy {
    public bool CanRemove(Employee employee) {
        ArgumentNullException.ThrowIfNull(employee);

        var from = employee.OrganizationUnit;

        return !from.HasDirectReports(employee);
    }

    public bool CanTransfer(Employee employee) {
        ArgumentNullException.ThrowIfNull(employee);

        var from = employee.OrganizationUnit;

        return !from.HasDirectReports(employee);
    }

    public void EnsureCanRemove(Employee employee) {
        ArgumentNullException.ThrowIfNull(employee);

        if (!CanRemove(employee)) {
            throw new DomainException("Cannot remove an employee who has direct reports.");
        }
    }

    public void EnsureCanTransfer(Employee employee) {
        ArgumentNullException.ThrowIfNull(employee);
        if (!CanTransfer(employee)) {
            throw new DomainException("Cannot transfer an employee who has direct reports.");
        }
    }
}