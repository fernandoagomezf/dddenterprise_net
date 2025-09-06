using System;

using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public record PersonName
    : ValueObject {
    public string FirstName { get; init; }
    public string LastName { get; init; }

    public PersonName(string firstName, string lastName) {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

        FirstName = firstName;
        LastName = lastName;
    }
}