
using System;

namespace VantagePoint.Domain.Common;

public abstract class Entity {
    private readonly Identifier _id;

    protected Entity(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        _id = id;
    }

    public Identifier Id => _id;

    public override bool Equals(object? obj) {
        if (obj is Entity other) {
            return Equals(other);
        }
        return false;
    }

    public bool Equals(Entity other) {
        if (other is null) return false;
        return _id.Equals(other._id);
    }

    public bool Equals(Identifier otherId) {
        return _id.Equals(otherId);
    }

    public override int GetHashCode() {
        return _id.GetHashCode();
    }
}

