using System;
using System.Linq;
using System.Threading.Tasks;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class EmployeeTransfer
    : Service {
    public EmployeeTransfer(IUnitOfWork unitOfWork)
        : base(unitOfWork) {
    }

    public void Transfer(Employee from, Employee to) {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);

        if (from.Id == to.Id) {
            throw new DomainException(from, "Cannot transfer an employee to themselves.");
        }
        if (to.Status != Status.Active) {
            throw new DomainException(to, "Target employee must be active to receive team members.");
        }

        while (from.DirectReports.Count > 0) {
            var directReport = from.DirectReports.First();
            from.DeassignDirectReport(directReport);
            to.AssignDirectReport(directReport);
        }
    }
}
