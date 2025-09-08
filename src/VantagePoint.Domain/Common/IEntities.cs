
using System;
using System.Collections.Generic;

namespace VantagePoint.Domain.Common;

public interface IEntities<T>
    : IEnumerable<IEntity>
    where T : IEntity {
    T Get(Guid id);
    T? Find(Guid id);
    int Count { get; }
}