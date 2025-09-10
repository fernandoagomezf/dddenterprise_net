using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed record Address
    : ValueObject {
    public static readonly Address Empty;
    public required string Street { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string PostalCode { get; init; }
    public required string Country { get; init; }

    static Address() {
        Empty = new Address {
            Street = string.Empty,
            City = string.Empty,
            State = string.Empty,
            PostalCode = string.Empty,
            Country = string.Empty
        };
    }

    public void Deconstruct(out string street, out string city, out string state, out string postalCode, out string country) {
        street = Street;
        city = City;
        state = State;
        postalCode = PostalCode;
        country = Country;
    }
}