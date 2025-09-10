
using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public sealed class Manager
    : Entity {
    private readonly PersonName _name;
    private readonly JobInformation _jobInfo;

    internal Manager(Identifier id, PersonName name, JobInformation jobInfo)
        : base(id) {
        ArgumentNullException.ThrowIfNull(name);
        _name = name;
        _jobInfo = jobInfo;
    }

    public PersonName Name => _name;
    public JobInformation JobInfo => _jobInfo;
}