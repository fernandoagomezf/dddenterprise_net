using System;
using System.Collections.Generic;

namespace VantagePoint.Domain.Common;

public interface IAggregateRoot
    : IEntity {
    IEntityCollection GetEntities();
    IEntity GetEntity(Guid id);
    IEntity? FindEntity(Guid id);
    IDomainEventCollection GetEvents();
    void Publish(DomainEvent domainEvent);
}
