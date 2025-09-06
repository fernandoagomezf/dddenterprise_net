using System;

namespace VantagePoint.Domain.Common;

public record DomainEvent
    : ValueObject {
    public string Context { get; init; }
    public string Code { get; init; }
    public DateTime Raised { get; init; }

    public DomainEvent(string context, string code, DateTime raised) {
        ArgumentException.ThrowIfNullOrWhiteSpace(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        Context = context;
        Code = code;
        Raised = raised;
    }

    public DomainEvent(string context, string code)
        : this(context, code, DateTime.Now) {

    }
}