using System;
using System.Collections;
using System.Collections.Generic;

namespace VantagePoint.Domain.Common;

public class ValueObjectCollection<T>
    : IEnumerable<T>, ICollection<T> where T : ValueObject {
    private readonly HashSet<T> _items;

    public ValueObjectCollection() {
        _items = new();
    }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
        => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => _items.GetEnumerator();

    public void Add(T item) {
        ArgumentNullException.ThrowIfNull(item);
        _items.Add(item);
    }

    public bool Remove(T item) {
        ArgumentNullException.ThrowIfNull(item);
        return _items.Remove(item);
    }

    public bool Contains(T item) {
        ArgumentNullException.ThrowIfNull(item);
        return _items.Contains(item);
    }

    public void Clear()
        => _items.Clear();

    void ICollection<T>.CopyTo(T[] array, int arrayIndex) {
        ArgumentNullException.ThrowIfNull(array);
        if (arrayIndex < 0 || arrayIndex + Count > array.Length) {
            throw new ArgumentOutOfRangeException();
        }
        foreach (var item in _items) {
            array[arrayIndex++] = item;
        }
    }

}
