using ChaidezMotorCompany.Domain.Models;

namespace ChaidezMotorCompany.Domain;

public interface ICarDomain
{
    public Task<IEnumerable<Car>> GetCars(CancellationToken cancellationToken);
}
