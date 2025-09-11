using System;

namespace VantagePoint.Domain.Common;

public abstract class Service {
    private readonly IUnitOfWork _unitOfWork;

    protected Service(IUnitOfWork unitOfWork) {
        ArgumentNullException.ThrowIfNull(unitOfWork);
        _unitOfWork = unitOfWork;
    }

    public IUnitOfWork UnitOfWork => _unitOfWork;
}