using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class Employee
    : IAggregateRoot {
    private readonly Guid _id;
    private PersonName _name;
    private PhoneNumber? _phoneNumber;
    private Employee? _manager;

    public Employee(PersonName name) {
        _id = Guid.NewGuid();
        _name = name;
        _phoneNumber = null;
        _manager = null;
    }

    public Employee(Guid id, PersonName name) {
        if (id == Guid.Empty) {
            throw new ArgumentException("The employee");
        }
        ArgumentNullException.ThrowIfNull(name);

        _name = name;
        _phoneNumber = null;
        _manager = null;
    }

    public Guid Id => _id;
    public PersonName Name => _name;
    public PhoneNumber? PhoneNumber => _phoneNumber;
    public Employee? Manager => _manager;

    Guid IEntity.Id => _id;

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