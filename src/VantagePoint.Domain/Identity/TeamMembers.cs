using System;
using System.Collections.Generic;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class TeamMembers
    : Entities<TeamMember>, ITeamMembers {
    public TeamMembers()
        : base() {
    }

    public TeamMembers(IEnumerable<TeamMember> members)
        : base(members) {
    }
}