using System;
using System.Linq;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class TeamTransferService
    : Service {
    public void TransferTeam(Employee from, Employee to) {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);

        if (to.Status != EmployeeStatus.Active) {
            throw new DomainException(to, "Target employee must be active to receive team members.");
        }
        while (from.TeamMembers.Count > 0) {
            var member = from.TeamMembers.First();
            from.RemoveTeamMember(member);
            to.AssignTeamMember(member);
        }
    }
}
