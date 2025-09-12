
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class EmployeeView
    : EntityView<Employee> {
    public EmployeeView(EmployeeCollection collection)
        : base(collection) {
    }
}