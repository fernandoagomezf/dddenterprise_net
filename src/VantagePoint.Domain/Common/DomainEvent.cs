using System;

namespace VantagePoint.Domain.Common;

public record DomainEvent(
    string Code,
    DateTime Raised
) : ValueObject();