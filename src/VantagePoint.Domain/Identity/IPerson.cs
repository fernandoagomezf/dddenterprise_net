
namespace VantagePoint.Domain.Identity;

public interface IPerson {
    PersonName Name { get; }
    Status Status { get; }
}