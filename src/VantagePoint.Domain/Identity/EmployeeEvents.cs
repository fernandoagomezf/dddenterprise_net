
using System.ComponentModel;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public static class EmployeeEvents {
    public static readonly DomainEvent InfoUpdated;
    public static readonly DomainEvent Promoted;
    public static readonly DomainEvent StatusChanged;
    public static readonly DomainEvent ManagerAssigned;
    public static readonly DomainEvent TeamMemberAssigned;
    public static readonly DomainEvent TeamMemberRemoved;

    static EmployeeEvents() {
        InfoUpdated = new("Identity", "Employee.InfoUpdated");
        Promoted = new("Identity", "Employee.Promoted");
        StatusChanged = new("Identity", "Employee.StatusChanged");
        ManagerAssigned = new("Identity", "Employee.ManagerAssigned");
        TeamMemberAssigned = new("Identity", "Employee.TeamMemberAssigned");
        TeamMemberRemoved = new("Identity", "Employee.TeamMemberRemoved");
    }
}