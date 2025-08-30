
using System;

namespace VantagePoint.Domain.Core;

public abstract class Entity {
    private Guid _id;

    protected Entity(Guid id) {
        if (id == default) {
            throw new DomainException("An entity must have a valid identity.");
        }
        _id = id;
    }

    protected Guid Id => _id;

    public override bool Equals(object? obj)
        => obj is Entity entity && Id.Equals(entity.Id);

    public bool Equals(Entity? other)
        => Equals((object?)other);

    public override int GetHashCode()
        => Id.GetHashCode();

    public static bool operator ==(Entity? left, Entity? right)
        => Equals(left, right);

    public static bool operator !=(Entity? left, Entity? right)
        => !Equals(left, right);
}