using System;
using System.Text.RegularExpressions;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public record Email
    : ValueObject {
    private static readonly Regex _regex;

    static Email() {
        _regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    }

    public string Value { get; init; }

    public Email(string value) {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (!_regex.IsMatch(value))
            throw new ArgumentException("Invalid email address format.", nameof(value));

        Value = value;
    }
}