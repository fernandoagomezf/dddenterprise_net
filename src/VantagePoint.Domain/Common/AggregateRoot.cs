
using System;

namespace VantagePoint.Domain.Common;

public abstract class AggregateRoot
    : Entity {
    private readonly DomainEventCollection _events;

    protected AggregateRoot()
        : base(Identifier.New()) {
        var ns = GetType()?.Namespace ?? String.Empty;
        ns = ns.Split('.')[^1] ?? String.Empty;
        _events = new(ns);
    }

    protected DomainEventCollection Events => _events;
}
