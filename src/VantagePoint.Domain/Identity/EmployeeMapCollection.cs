using System;
using System.Linq;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class EmployeeMapCollection
    : ValueObjectCollection<EmployeeMap> {
    private readonly OrganizationUnit _organizationUnit;

    public EmployeeMapCollection(OrganizationUnit organizationUnit) {
        ArgumentNullException.ThrowIfNull(organizationUnit);
        _organizationUnit = organizationUnit;
    }

    public OrganizationUnit OrganizationUnit => _organizationUnit;

    public override void Add(EmployeeMap item) {
        ArgumentNullException.ThrowIfNull(item);
        if (item.OrganizationUnit != _organizationUnit.Id) {
            throw new DomainException("Employee map organization unit does not match collection organization unit.");
        }
        base.Add(item);
    }

    public override bool Remove(EmployeeMap item) {
        ArgumentNullException.ThrowIfNull(item);
        if (item.OrganizationUnit != _organizationUnit.Id) {
            throw new DomainException("Employee map organization unit does not match collection organization unit.");
        }
        return base.Remove(item);
    }

    public bool ContainsMap(Employee manager, Employee report) {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(report);
        if (manager.OrganizationUnit.Id != _organizationUnit.Id || report.OrganizationUnit.Id != _organizationUnit.Id) {
            throw new DomainException("Both employees must belong to the collection organization unit.");
        }
        var map = new EmployeeMap(manager, report);
        return Contains(map);
    }

    public EmployeeMap? Find(Employee report) {
        ArgumentNullException.ThrowIfNull(report);
        if (report.OrganizationUnit.Id != _organizationUnit.Id) {
            throw new DomainException("Employee must belong to the collection organization unit.");
        }
        var map = this.Where(x => x.Report == report.Id).FirstOrDefault();
        return map;
    }

    public bool ContainsManager(Employee manager) {
        ArgumentNullException.ThrowIfNull(manager);
        if (manager.OrganizationUnit.Id != _organizationUnit.Id) {
            throw new DomainException("Employee must belong to the collection organization unit.");
        }
        var contains = this.Where(x => x.Manager == manager.Id).Any();
        return contains;
    }

    public bool ContainsReport(Employee report) {
        ArgumentNullException.ThrowIfNull(report);
        if (report.OrganizationUnit.Id != _organizationUnit.Id) {
            throw new DomainException("Employee must belong to the collection organization unit.");
        }
        var contains = this.Where(x => x.Report == report.Id).Any();
        return contains;
    }
}