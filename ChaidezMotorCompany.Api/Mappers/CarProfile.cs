using AutoMapper;
using ChaidezMotorCompany.Domain.Models;
using ChaidezMotorCompany.Infrastructure;

namespace ChaidezMotorCompany.Api;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CarDto, Car>();
        CreateMap<Car, CarViewModel>();
    }
}
