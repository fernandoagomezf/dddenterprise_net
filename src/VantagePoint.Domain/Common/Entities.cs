using System;
using System.Collections;
using System.Collections.Generic;

namespace VantagePoint.Domain.Common;

public class Entities<T>
    : IEnumerable<T>, IReadOnlyCollection<T> where T : Entity {
    private readonly EntityCollection<T> _items;

    public Entities(EntityCollection<T> entities) {
        ArgumentNullException.ThrowIfNull(entities);
        _items = entities;
    }

    public AggregateRoot Root => _items.Root;
    public int Count => _items.Count;
    public bool IsReadOnly => true;

    IEnumerator<T> IEnumerable<T>.GetEnumerator() {
        return ((IEnumerable<T>)_items).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return ((IEnumerable<T>)_items).GetEnumerator();
    }

    public bool Contains(T item) {
        ArgumentNullException.ThrowIfNull(item);
        return _items.Contains(item);
    }

    public bool Contains(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        return _items.Contains(id);
    }

    public T Get(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        return _items.Get(id);
    }

    public T? Find(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        return _items.Find(id);
    }
}