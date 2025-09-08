
using System;

namespace VantagePoint.Domain.Common;

public abstract class Entity
    : IEntity {
    private readonly Guid _id;

    protected Entity(Guid id) {
        if (id == Guid.Empty) {
            throw new ArgumentException($"The entity must have a valid ID.");
        }
        _id = id;
    }

    public Guid Id => _id;

    bool IEquatable<IEntity>.Equals(IEntity? other) {
        if (other is null) return false;
        return _id == other.Id;
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is IEntity entity)
            return ((IEquatable<IEntity>)this).Equals(entity);
        return false;
    }

    public override int GetHashCode()
        => _id.GetHashCode();
}