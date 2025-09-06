
using System;
using System.Collections.Generic;

namespace VantagePoint.Domain.Common;

public abstract class AggregateRoot
    : Entity, IAggregateRoot {
    private readonly DomainEventCollection _events;
    protected AggregateRoot()
        : base(Guid.NewGuid()) {
        _events = new();
    }

    protected AggregateRoot(Guid id)
        : base(id) {
        _events = new();
    }

    protected void Publish(DomainEvent domainEvent) {
        ArgumentNullException.ThrowIfNull(domainEvent);
        _events.Add(domainEvent);
    }
}