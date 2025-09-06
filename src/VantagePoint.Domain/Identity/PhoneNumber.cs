
using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public record PhoneNumber
    : ValueObject {
    public string CountryCode { get; init; }
    public string MainNumber { get; init; }
    public string Extension { get; init; }

    public PhoneNumber(string mainNumber, string countryCode, string extension) {
        ArgumentException.ThrowIfNullOrWhiteSpace(mainNumber);
        MainNumber = mainNumber;
        CountryCode = countryCode;
        Extension = extension;
    }

    public PhoneNumber(string mainNumber)
        : this(mainNumber, String.Empty, String.Empty) {

    }
}