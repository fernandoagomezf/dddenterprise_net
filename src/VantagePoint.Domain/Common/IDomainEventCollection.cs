
using System.Collections.Generic;

namespace VantagePoint.Domain.Common;

public interface IDomainEventCollection
    : IEnumerable<DomainEvent> {

}