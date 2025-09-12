using System;
using System.Text.RegularExpressions;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Organization;

public sealed record Email
    : ValueObject {
    private static readonly Regex _regex;
    public static readonly Email Empty;
    public string Value { get; init; }

    static Email() {
        _regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        Empty = new();
    }

    private Email() {
        Value = String.Empty;
    }

    public Email(string value) {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (!_regex.IsMatch(value))
            throw new ArgumentException("Invalid email address format.", nameof(value));

        Value = value;
    }
}