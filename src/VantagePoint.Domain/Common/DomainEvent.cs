using System;

namespace VantagePoint.Domain.Common;

public sealed record DomainEvent
    : ValueObject {
    public string Context { get; init; }
    public string Code { get; init; }
    public DateTime Raised { get; init; }

    public DomainEvent(string context, string code, DateTime raised) {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(code);
        if (String.IsNullOrWhiteSpace(context)) {
            throw new ArgumentException("The domain event context must be provided.", nameof(context));
        }
        if (String.IsNullOrWhiteSpace(code)) {
            throw new ArgumentException("The domain event code must be provided.", nameof(code));
        }
        Context = context;
        Code = code;
        Raised = raised;
    }

    public DomainEvent(string context, string code)
        : this(context, code, DateTime.Now) {

    }

    public void Deconstruct(out string context, out string code, out DateTime raised) {
        context = Context;
        code = Code;
        raised = Raised;
    }
}