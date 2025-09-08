
using System;

namespace VantagePoint.Domain.Common;

public interface IEntityCollection<T>
    : IEntities<T>
    where T : IEntity {
    void Add(T entity);
    void Remove(Guid id);
    bool Contains(Guid id);
    bool Contains(T entity);
}