using System.Collections.Generic;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class EmployeeCollection
    : EntityCollection<Employee> {
    public EmployeeCollection()
        : base() {
    }

    public EmployeeCollection(IEnumerable<Employee> items)
        : base(items) {
    }
}