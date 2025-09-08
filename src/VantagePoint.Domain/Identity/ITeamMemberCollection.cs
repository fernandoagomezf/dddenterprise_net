
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public interface ITeamMemberCollection
    : ITeamMembers, IEntityCollection<TeamMember> {

}