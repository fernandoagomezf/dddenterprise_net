using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed record JobInformation
    : ValueObject {
    public static readonly JobInformation Empty;
    public required string JobTitle { get; init; }
    public required string Department { get; init; }
    public required string Location { get; init; }
    public required DateTime HiredDate { get; init; }

    static JobInformation() {
        Empty = new JobInformation {
            JobTitle = String.Empty,
            Department = String.Empty,
            Location = String.Empty,
            HiredDate = DateTime.MinValue
        };
    }
}