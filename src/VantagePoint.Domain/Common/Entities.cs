
using System;
using System.Collections;
using System.Collections.Generic;

namespace VantagePoint.Domain.Common;

public class Entities<T>
    : IEntities<T>
    where T : IEntity {
    private readonly Dictionary<Guid, T> _items;

    protected Entities() {
        _items = new();
    }

    protected Entities(IEnumerable<T> entities) {
        ArgumentNullException.ThrowIfNull(entities);
        _items = new();
        foreach (var entity in entities) {
            _items[entity.Id] = entity;
        }
    }

    protected Dictionary<Guid, T> Items => _items;

    public virtual T Get(Guid id) {
        if (!_items.TryGetValue(id, out var entity)) {
            throw new KeyNotFoundException($"The entity with ID '{id}' was not found.");
        }
        return entity;
    }

    public virtual T? Find(Guid id) {
        _items.TryGetValue(id, out var entity);
        return entity;
    }

    public IEnumerator<T> GetEnumerator()
        => _items.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _items.Values.GetEnumerator();

    IEnumerator<IEntity> IEnumerable<IEntity>.GetEnumerator() {
        throw new NotImplementedException();
    }

    public int Count => _items.Count;
}