using System.Collections.Generic;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class EmployeeCollection
    : EntityCollection<EmployeeInfo> {
    public EmployeeCollection(Employee employee)
        : base(employee) {
    }

    public EmployeeCollection(Employee employee, IEnumerable<EmployeeInfo> items)
        : base(employee, items) {
    }
}