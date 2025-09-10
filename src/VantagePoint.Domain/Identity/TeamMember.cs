
using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed class TeamMember
    : Entity {
    private readonly PersonName _name;

    public TeamMember(Identifier id, PersonName name)
        : base(id) {
        ArgumentNullException.ThrowIfNull(name);
        _name = name;
    }

    public PersonName Name => _name;
}