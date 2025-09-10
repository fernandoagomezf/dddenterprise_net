using System;

namespace VantagePoint.Domain.Common;

public sealed record Identifier
    : ValueObject {
    public Guid Value { get; init; }

    public Identifier(Guid value) {
        if (value == Guid.Empty) {
            throw new ArgumentException("Identifier value cannot be empty.", nameof(value));
        }
        Value = value;
    }

    public override string ToString() => Value.ToString();

    public static Identifier New() => new Identifier(Guid.NewGuid());
}