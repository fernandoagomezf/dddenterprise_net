
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class Employees
    : Entities<Employee> {
    public Employees(EmployeeCollection collection)
        : base(collection) {
    }
}