using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VantagePoint.Domain.Common;

public class EntityCollection<T>
    : IEnumerable<T>, ICollection<T> where T : Entity {
    private readonly Dictionary<Identifier, T> _items;

    public EntityCollection() {
        _items = new();
    }

    public EntityCollection(IEnumerable<T> items) {
        ArgumentNullException.ThrowIfNull(items);
        _items = items.ToDictionary(x => x.Id);
    }

    public int Count => _items.Count;
    public bool IsReadOnly => false;

    IEnumerator<T> IEnumerable<T>.GetEnumerator() {
        return _items.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return _items.Values.GetEnumerator();
    }

    public void Add(T item) {
        ArgumentNullException.ThrowIfNull(item);
        _items.Add(item.Id, item);
    }

    public void Clear() {
        _items.Clear();
    }

    public bool Contains(T item) {
        ArgumentNullException.ThrowIfNull(item);
        return _items.ContainsKey(item.Id);
    }

    public bool Contains(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        return _items.ContainsKey(id);
    }

    public bool Remove(T item) {
        ArgumentNullException.ThrowIfNull(item);
        return _items.Remove(item.Id);
    }

    public bool Remove(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        return _items.Remove(id);
    }

    void ICollection<T>.CopyTo(T[] array, int arrayIndex) {
        ArgumentNullException.ThrowIfNull(array);
        if (arrayIndex < 0 || arrayIndex + Count > array.Length) {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }
        _items.Values.CopyTo(array, arrayIndex);
    }

    public T Get(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        if (!_items.TryGetValue(id, out var item)) {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
        return item;
    }

    public T? Find(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        if (!_items.TryGetValue(id, out var item)) {
            return null;
        }
        return item;
    }
}
