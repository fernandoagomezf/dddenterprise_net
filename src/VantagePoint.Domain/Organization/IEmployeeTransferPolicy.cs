using System;

namespace VantagePoint.Domain.Organization;

public interface IEmployeeTransferPolicy {
    bool CanTransfer(Employee employee);
    void EnsureCanTransfer(Employee employee);
    bool CanRemove(Employee employee);
    void EnsureCanRemove(Employee employee);
}