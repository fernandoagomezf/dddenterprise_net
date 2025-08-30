using System;

namespace VantagePoint.Domain.Core;

public record DomainEvent(
    string Code,
    DateTime Raised
) : ValueObject();