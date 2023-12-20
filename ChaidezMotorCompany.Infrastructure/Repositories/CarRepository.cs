using AutoMapper;
using ChaidezMotorCompany.Domain;
using ChaidezMotorCompany.Domain.Models;

namespace ChaidezMotorCompany.Infrastructure;

public class CarRepository : ICarRepository
{
    private readonly IMapper _mapper;

    public CarRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<IEnumerable<Car>> GetCars(CancellationToken cancellationToken)
    {
        List<CarDto> cars =
         [
             new CarDto{
                Model = "CRS",
                Trim = "MX",
                Year = 2023
                },
            new CarDto{
                Model = "DRG",
                Trim = "MX",
                Year = 2023
                },
         ];
         return _mapper.Map<IEnumerable<Car>>(cars);
    }
}
