
using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed class Manager
    : Entity {
    private readonly PersonName _name;

    public Manager(Identifier id, PersonName name)
        : base(id) {
        ArgumentNullException.ThrowIfNull(name);
        _name = name;
    }

    public PersonName Name => _name;
}