
using System.ComponentModel;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public static class EmployeeEvents {
    public static readonly DomainEvent Updated;
    public static readonly DomainEvent Promoted;

    static EmployeeEvents() {
        Updated = new("Identity", "Employee.Updated");
        Promoted = new("Identity", "Employee.Promoted");
    }
}