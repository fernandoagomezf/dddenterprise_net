using System;
using System.Threading;
using System.Threading.Tasks;

namespace VantagePoint.Domain.Common;

public interface IUnitOfWork
    : IAsyncDisposable {
    void Commit();
}