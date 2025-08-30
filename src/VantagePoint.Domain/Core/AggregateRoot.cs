using System;
using VantagePoint.Domain.Core;

public abstract class AggregateRoot
    : Entity {
    private DomainEventCollection _events;

    protected AggregateRoot()
        : base(Guid.NewGuid()) {
        _events = new();
    }

    protected AggregateRoot(Guid id)
        : base(id) {
        _events = new();
    }
}