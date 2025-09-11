using System;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Identity;

public class EmployeeInfo
    : Entity, IPerson {
    private readonly PersonName _name;
    private readonly Status _status;

    public EmployeeInfo(Identifier id, PersonName name, Status status)
        : base(id) {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(name);
        _name = name;
        _status = status;
    }

    public PersonName Name => _name;
    public Status Status => _status;
}