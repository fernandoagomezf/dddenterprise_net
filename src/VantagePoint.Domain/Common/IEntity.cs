
using System;

namespace VantagePoint.Domain.Common;

public interface IEntity
    : IEquatable<IEntity> {
    Guid Id { get; }
}

