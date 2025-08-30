using System.Collections;
using System.Collections.Generic;

namespace VantagePoint.Domain.Core;

public class DomainEventCollection
    : IEnumerable<DomainEvent> {
    private readonly AggregateRoot _owner;
    private readonly Queue<DomainEvent> _events;

    public DomainEventCollection(AggregateRoot owner) {
        _owner = owner;
        _events = new();
    }

    public AggregateRoot Owner => _owner;

    public void Publish(DomainEvent domainEvent) {
        _events.Enqueue(domainEvent);
    }

    public DomainEvent Handle()
        => _events.Dequeue();

    public IEnumerator<DomainEvent> GetEnumerator()
        => _events.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}