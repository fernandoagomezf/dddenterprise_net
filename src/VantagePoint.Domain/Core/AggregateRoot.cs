using System;
using VantagePoint.Domain.Core;

public abstract class AggregateRoot
    : Entity {
    private DomainEventCollection _events;
    private EntityCollection _entities;

    protected AggregateRoot()
        : base(Guid.NewGuid()) {
        _events = new(this);
        _entities = new(this);
    }

    protected AggregateRoot(Guid id)
        : base(id) {
        _events = new(this);
    }

    protected DomainEventCollection Events => _events;
}