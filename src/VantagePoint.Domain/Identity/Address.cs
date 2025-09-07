using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public record Address
    : ValueObject {
    public static readonly Address Empty;
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string PostalCode { get; init; }
    public string Country { get; init; }

    static Address() {
        Empty = new();
    }

    private Address() {
        Street = String.Empty;
        City = String.Empty;
        State = String.Empty;
        Country = String.Empty;
        PostalCode = String.Empty;
    }

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