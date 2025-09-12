using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Organization;

public sealed record JobInformation
    : ValueObject {
    public static readonly JobInformation Empty;
    public string JobTitle { get; init; }
    public string Department { get; init; }
    public string Location { get; init; }
    public DateTime HiredDate { get; init; }

    static JobInformation() {
        Empty = new();
    }

    private JobInformation() {
        JobTitle = String.Empty;
        Department = String.Empty;
        Location = String.Empty;
        HiredDate = DateTime.MinValue;
    }

    public JobInformation(string title) {
        ArgumentNullException.ThrowIfNull(title);
        JobTitle = title;
        Department = String.Empty;
        Location = String.Empty;
        HiredDate = DateTime.MinValue;
    }

    public void Deconstruct(out string jobTitle, out string department, out string location, out DateTime hiredDate) {
        ArgumentNullException.ThrowIfNull(JobTitle);
        ArgumentNullException.ThrowIfNull(Department);
        ArgumentNullException.ThrowIfNull(Location);
        jobTitle = JobTitle;
        department = Department;
        location = Location;
        hiredDate = HiredDate;
    }
}