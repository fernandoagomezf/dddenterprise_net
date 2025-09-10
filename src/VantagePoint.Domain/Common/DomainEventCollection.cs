
using System;
using System.Collections;
using System.Collections.Generic;

namespace VantagePoint.Domain.Common;

public class DomainEventCollection
    : IEnumerable<DomainEvent> {
    private readonly List<DomainEvent> _events;
    private readonly string _context;

    public DomainEventCollection(string context)
        : base() {
        _context = context;
        _events = new();
    }

    public string Context => _context;

    public void AddEvent(DomainEvent domainEvent) {
        ArgumentNullException.ThrowIfNull(domainEvent);
        _events.Add(domainEvent);
    }

    public void AddEvent(string code) {
        if (String.IsNullOrWhiteSpace(code)) {
            throw new ArgumentException("The domain event code must be provided.", nameof(code));
        }
        _events.Add(new DomainEvent(_context, code));
    }

    public void Clear() {
        _events.Clear();
    }

    IEnumerator<DomainEvent> IEnumerable<DomainEvent>.GetEnumerator() {
        return _events.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return _events.GetEnumerator();
    }
}