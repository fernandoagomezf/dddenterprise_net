using System;
using System.Collections.Generic;

namespace VantagePoint.Domain.Common;

public class EntityCollection<T>
    : Entities<T>, IEntityCollection<T>
    where T : IEntity {
    public EntityCollection()
        : base() {
    }

    public EntityCollection(IEnumerable<T> entities)
        : base(entities) {
    }

    public virtual void Add(T entity) {
        ArgumentNullException.ThrowIfNull(entity);
        Items[entity.Id] = entity;
    }

    public void Remove(Guid id) {
        Items.Remove(id);
    }

    public bool Contains(Guid id) {
        return Items.ContainsKey(id);
    }

    public bool Contains(T entity) {
        ArgumentNullException.ThrowIfNull(entity);
        return Contains(entity.Id);
    }
}