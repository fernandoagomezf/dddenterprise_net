using System;
using System.Linq;

namespace VantagePoint.Domain.Common;

public class DomainException
    : Exception {
    public DomainException()
        : this("A problem ocurred within the domain.") {
    }

    public DomainException(string message)
        : base(message) {
    }

    public DomainException(string boundContext, string entity, string id, string message)
        : this(message) {
        BoundContext = boundContext;
        Entity = entity;
        Id = id;
    }

    public DomainException(Entity entity, string message)
        : this(message) {
        ArgumentNullException.ThrowIfNull(entity);
        var type = entity.GetType();
        BoundContext = type.Namespace?.Split('.')?.Last() ?? String.Empty;
        Entity = type.Name;
        Id = entity.Id.Value.ToString();
    }

    public DomainException(string message, Exception innerException)
        : base(message, innerException) {
    }

    public string BoundContext {
        get => Data[nameof(BoundContext)] as string ?? String.Empty;
        set => Data[nameof(BoundContext)] = value;
    }

    public string Entity {
        get => Data[nameof(Entity)] as string ?? String.Empty;
        set => Data[nameof(Entity)] = value;
    }

    public string Id {
        get => Data[nameof(Id)] as string ?? String.Empty;
        set => Data[nameof(Id)] = value;
    }
}