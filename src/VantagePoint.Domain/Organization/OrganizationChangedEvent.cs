using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Organization;

public record OrganizationChangedEvent
    : DomainEvent {
    public Identifier OrganizationUnit { get; init; }

    public OrganizationChangedEvent(Identifier organizationUnit)
        : base("Identity", "OrganizationUnit.OrganizationChangedEvent") {
        ArgumentNullException.ThrowIfNull(organizationUnit);
        OrganizationUnit = organizationUnit;
    }

    public void Deconstruct(out Identifier organizationUnit) {
        organizationUnit = OrganizationUnit;
    }
}