
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class TeamMemberCollection
    : EntityCollection<TeamMember>, ITeamMemberCollection {

    public TeamMemberCollection()
        : base() {
    }

    public TeamMemberCollection(IEnumerable<TeamMember> teamMembers)
        : base(teamMembers) {
    }
}