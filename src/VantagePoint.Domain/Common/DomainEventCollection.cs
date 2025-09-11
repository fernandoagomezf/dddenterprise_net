
using System;

namespace VantagePoint.Domain.Common;

public class DomainEventCollection
    : ValueObjectCollection<DomainEvent> {

    public DomainEventCollection()
        : base() {
    }

    public void Add(string context, string code) {
        ArgumentException.ThrowIfNullOrWhiteSpace(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        Add(new DomainEvent(context, code));
    }

}