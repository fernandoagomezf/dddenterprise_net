
using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Organization;

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

    /// <summary>
    /// Deconstructs the phone number into main number, country code and extension.
    /// </summary>
    /// <param name="mainNumber">The main phone number.</param>
    /// <param name="countryCode">The country code.</param>
    /// <param name="extension">The extension.</param>
    public void Deconstruct(out string mainNumber, out string countryCode, out string extension) {
        ArgumentNullException.ThrowIfNull(MainNumber);
        ArgumentNullException.ThrowIfNull(CountryCode);
        ArgumentNullException.ThrowIfNull(Extension);

        mainNumber = MainNumber;
        countryCode = CountryCode;
        extension = Extension;
    }

    static PhoneNumber() {
        Empty = new();
    }
}