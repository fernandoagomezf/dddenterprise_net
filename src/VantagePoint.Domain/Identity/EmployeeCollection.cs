using System.Collections.Generic;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Organization;

public class EmployeeCollection
    : EntityCollection<Employee> {
    public EmployeeCollection()
        : base() {
    }

    public EmployeeCollection(IEnumerable<Employee> items)
        : base(items) {
    }
}