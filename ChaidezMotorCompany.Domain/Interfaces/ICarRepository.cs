using ChaidezMotorCompany.Domain.Models;

namespace ChaidezMotorCompany.Domain;

public interface ICarRepository
{
    Task<IEnumerable<Car>> GetCars(CancellationToken cancellationToken);
}
