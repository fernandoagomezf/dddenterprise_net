
using System.ComponentModel;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public static class EmployeeEvents {
    public static readonly DomainEvent Updated;
    public static readonly DomainEvent Promoted;
    public static readonly DomainEvent StatusChanged;
    public static readonly DomainEvent AssignedToTeam;
    public static readonly DomainEvent TeamTransferred;
    public static readonly DomainEvent TeamMemberAssigned;
    public static readonly DomainEvent TeamMemberRemoved;

    static EmployeeEvents() {
        Updated = new("Identity", "Employee.Updated");
        Promoted = new("Identity", "Employee.Promoted");
        StatusChanged = new("Identity", "Employee.StatusChanged");
        AssignedToTeam = new("Identity", "Employee.AssignedToTeam");
        TeamTransferred = new("Identity", "Employee.TeamTransferred");
        TeamMemberAssigned = new("Identity", "Employee.TeamMemberAssigned");
        TeamMemberRemoved = new("Identity", "Employee.TeamMemberRemoved");
    }
}