
using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed record PhoneNumber
    : ValueObject {
    public static readonly PhoneNumber Empty;
    public string CountryCode { get; init; }
    public string MainNumber { get; init; }
    public string Extension { get; init; }

    private PhoneNumber() {
        CountryCode = String.Empty;
        MainNumber = String.Empty;
        Extension = String.Empty;
    }

    public PhoneNumber(string mainNumber, string countryCode, string extension) {
        ArgumentException.ThrowIfNullOrWhiteSpace(mainNumber);
        MainNumber = mainNumber;
        CountryCode = countryCode;
        Extension = extension;
    }

    public PhoneNumber(string mainNumber)
        : this(mainNumber, String.Empty, String.Empty) {

    }

    static PhoneNumber() {
        Empty = new();
    }
}