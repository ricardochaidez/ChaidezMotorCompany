using ChaidezMotorCompany.Domain;
using ChaidezMotorCompany.Domain.Models;

namespace ChaidezMotorCompany.Infrastructure;

public class CarRepository : ICarRepository
{
    public async Task<IEnumerable<Car>> GetCars(CancellationToken cancellationToken)
    {
        List<Car> cars =
         [
             new Car{
                Model = "CRS",
                Trim = "MX",
                Year = 2023
                },
            new Car{
                Model = "DRG",
                Trim = "MX",
                Year = 2023
                },
         ];
         return cars;
    }
}
