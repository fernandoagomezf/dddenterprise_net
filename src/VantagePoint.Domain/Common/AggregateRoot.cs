
using System;

namespace VantagePoint.Domain.Common;

public abstract class AggregateRoot
    : Entity {
    private readonly DomainEventCollection _events;

    protected AggregateRoot()
        : base(Identifier.New()) {
        _events = new();
    }

    protected DomainEventCollection Events => _events;
}
