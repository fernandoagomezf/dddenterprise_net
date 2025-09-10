
using System;

namespace VantagePoint.Domain.Common;

public abstract class AggregateRoot
    : Entity {
    private readonly DomainEventCollection _events;

    protected AggregateRoot()
        : base(Identifier.New()) {
        _events = new(Context);
    }

    protected string Context {
        get {
            var ns = GetType()?.Namespace ?? String.Empty;
            return ns.Split('.')[^1] ?? String.Empty;
        }
    }

    protected DomainEventCollection Events => _events;
}
