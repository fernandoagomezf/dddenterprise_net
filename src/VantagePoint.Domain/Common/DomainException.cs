using System;

namespace VantagePoint.Domain.Common;

public class DomainException
    : Exception {
    public DomainException()
        : this("A problem ocurred within the domain.") {
    }

    public DomainException(string message)
        : base(message) {
    }

    public DomainException(string message, Exception innerException)
        : base(message, innerException) {
    }
}