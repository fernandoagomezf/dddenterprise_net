using System;
using System.Collections;
using System.Collections.Generic;

namespace VantagePoint.Domain.Core;

public class EntityCollection
    : IEnumerable<Entity> {
    private readonly AggregateRoot _owner;

    public EntityCollection(AggregateRoot owner) {
        _owner = owner;
    }

    public AggregateRoot Owner => _owner;


    public IEnumerator<Entity> GetEnumerator() {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}