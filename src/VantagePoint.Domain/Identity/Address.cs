using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public record Address
    : ValueObject {
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string PostalCode { get; init; }
    public string Country { get; init; }

    public Address(string street, string city, string state, string country, string postalCode) {
        ArgumentException.ThrowIfNullOrWhiteSpace(street);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        ArgumentException.ThrowIfNullOrWhiteSpace(state);
        ArgumentException.ThrowIfNullOrWhiteSpace(postalCode);
        Street = street;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
    }
}