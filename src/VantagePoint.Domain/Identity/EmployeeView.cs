
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Organization;

public class EmployeeView
    : EntityView<Employee> {
    public EmployeeView(EmployeeCollection collection)
        : base(collection) {
    }
}