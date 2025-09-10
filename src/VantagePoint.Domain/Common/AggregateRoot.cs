
using System;

namespace VantagePoint.Domain.Common;

public abstract class AggregateRoot
    : Entity {
    private readonly DomainEventCollection _events;
    private readonly EntityCollection _entities;

    protected AggregateRoot()
        : base(Identifier.New()) {
        var ns = GetType()?.Namespace ?? String.Empty;
        ns = ns.Split('.')[^1] ?? String.Empty;
        _events = new(ns);
        _entities = new(this);
    }

    protected DomainEventCollection Events => _events;
    protected EntityCollection Entities => _entities;
}
