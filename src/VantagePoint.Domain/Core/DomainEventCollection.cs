using System.Collections;
using System.Collections.Generic;

namespace VantagePoint.Domain.Core;

public class DomainEventCollection
    : IEnumerable<DomainEvent> {
    private Queue<DomainEvent> _events;

    public DomainEventCollection() {
        _events = new();
    }

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