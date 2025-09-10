using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VantagePoint.Domain.Common;

public class EntityCollection
    : IEnumerable<Entity>, ICollection<Entity> {
    private readonly AggregateRoot _root;
    private readonly Dictionary<Identifier, Entity> _items;

    public EntityCollection(AggregateRoot root) {
        ArgumentNullException.ThrowIfNull(root);
        _root = root;
        _items = new();
    }

    public EntityCollection(AggregateRoot root, IEnumerable<Entity> items)
        : this(root) {
        ArgumentNullException.ThrowIfNull(items);
        _items = items.ToDictionary(x => x.Id);
    }

    public AggregateRoot Root => _root;
    public int Count => _items.Count;
    public bool IsReadOnly => false;

    IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator() {
        return _items.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return _items.Values.GetEnumerator();
    }

    public void Add(Entity item) {
        ArgumentNullException.ThrowIfNull(item);
        _items.Add(item.Id, item);
    }

    public void Clear() {
        _items.Clear();
    }

    public bool Contains(Entity item) {
        ArgumentNullException.ThrowIfNull(item);
        return _items.ContainsKey(item.Id);
    }


    public bool Contains(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        return _items.ContainsKey(id);
    }

    public bool Remove(Entity item) {
        ArgumentNullException.ThrowIfNull(item);
        return _items.Remove(item.Id);
    }

    public bool Remove(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        return _items.Remove(id);
    }

    void ICollection<Entity>.CopyTo(Entity[] array, int arrayIndex) {
        ArgumentNullException.ThrowIfNull(array);
        if (arrayIndex < 0 || arrayIndex + Count > array.Length) {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }
        _items.Values.CopyTo(array, arrayIndex);
    }

    public Entity Get(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        if (!_items.TryGetValue(id, out var item)) {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
        return item;
    }

    public T Get<T>(Identifier id) where T : Entity {
        ArgumentNullException.ThrowIfNull(id);
        if (!_items.TryGetValue(id, out var item)) {
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
        }
        if (item is not T) {
            throw new InvalidCastException($"Entity with ID {id} is not of type {typeof(T).Name}.");
        }
        return (T)item;
    }

    public Entity? Find(Identifier id) {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(id);
        if (!_items.TryGetValue(id, out var item)) {
            return null;
        }
        return item;
    }

    public T? Find<T>(Identifier id) where T : Entity {
        ArgumentNullException.ThrowIfNull(id);
        if (!_items.TryGetValue(id, out var item)) {
            return null;
        }
        if (item is not T) {
            return null;
        }
        return (T)item;
    }

    public Entities<T> AsEntities<T>() where T : Entity {
        var items = _items.Values.OfType<T>().ToList();
        return new Entities<T>(Root, items);
    }
}
