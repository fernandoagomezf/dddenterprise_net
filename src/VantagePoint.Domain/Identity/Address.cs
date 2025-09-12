using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Organization;

public sealed record Address
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
        PostalCode = String.Empty;
        Country = String.Empty;
    }

    public Address(string street)
        : this(street, String.Empty, String.Empty, String.Empty, String.Empty) {

    }

    public Address(string street, string city, string state, string postalCode, string country) {
        ArgumentNullException.ThrowIfNull(street);
        ArgumentNullException.ThrowIfNull(city);
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(postalCode);
        ArgumentNullException.ThrowIfNull(country);
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    public void Deconstruct(out string street, out string city, out string state, out string postalCode, out string country) {
        street = Street;
        city = City;
        state = State;
        postalCode = PostalCode;
        country = Country;
    }
}