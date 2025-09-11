using System;
using System.Threading;
using System.Threading.Tasks;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public interface IEmployeeRepository
    : IRepository<Employee> {

}