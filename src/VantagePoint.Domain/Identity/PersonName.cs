using System;

using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed record PersonName
    : ValueObject {
    public static readonly PersonName Empty;
    public string FirstName { get; init; }
    public string LastName { get; init; }

    static PersonName() {
        Empty = new();
    }

    public PersonName(string firstName, string lastName) {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

        FirstName = firstName;
        LastName = lastName;
    }

    private PersonName() {
        FirstName = String.Empty;
        LastName = String.Empty;
    }

    public void Deconstruct(out string firstName, out string lastName) {
        firstName = FirstName;
        lastName = LastName;
    }
}