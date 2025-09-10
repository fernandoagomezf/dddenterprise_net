using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VantagePoint.Domain.Common;

public class Entities<T>
    : IEnumerable<T>, IReadOnlyCollection<T> where T : Entity {
    private readonly AggregateRoot _root;
    private readonly List<T> _items;

    public Entities(AggregateRoot root) {
        ArgumentNullException.ThrowIfNull(root);
        _root = root;
        _items = new();
    }

    public Entities(AggregateRoot root, IEnumerable<T> items)
        : this(root) {
        ArgumentNullException.ThrowIfNull(items);
        _items = [.. items];
    }

    public AggregateRoot Root => _root;
    public int Count => _items.Count;
    public bool IsReadOnly => true;

    IEnumerator<T> IEnumerable<T>.GetEnumerator() {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return _items.GetEnumerator();
    }

    public bool Contains(T item) {
        ArgumentNullException.ThrowIfNull(item);
        return _items.Contains(item);
    }

    public bool Contains(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        return _items.Where(x => x.Id == id)
            .Any();
    }

    public T Get(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        var item = _items.Where(x => x.Id == id)
            .FirstOrDefault();
        if (item is null) {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
        return item;
    }

    public T? Find(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        return _items.Where(x => x.Id == id)
            .FirstOrDefault();
    }
}