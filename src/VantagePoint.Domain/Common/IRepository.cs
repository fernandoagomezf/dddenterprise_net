
namespace VantagePoint.Domain.Common;

public interface IRepository<T>
    where T : AggregateRoot {
    IUnitOfWork UnitOfWork { get; }
    T GetById(Identifier id);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}