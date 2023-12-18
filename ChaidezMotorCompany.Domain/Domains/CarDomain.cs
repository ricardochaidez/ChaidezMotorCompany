using ChaidezMotorCompany.Domain.Models;

namespace ChaidezMotorCompany.Domain;

public class CarDomain : ICarDomain
{
    private readonly ICarRepository _carRepository;
    public CarDomain(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<IEnumerable<Car>> GetCars(CancellationToken cancellationToken)
    {
        return await _carRepository.GetCars(cancellationToken);
    }
}
