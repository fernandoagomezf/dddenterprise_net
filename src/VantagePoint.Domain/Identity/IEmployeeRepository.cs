using System.Collections.Generic;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public interface IEmployeeRepository
    : IRepository<Employee> {
    IEnumerable<Employee> GetDirectReports(Identifier managerId);
}