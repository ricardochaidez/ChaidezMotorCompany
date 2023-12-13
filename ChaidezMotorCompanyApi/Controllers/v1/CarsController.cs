using Microsoft.AspNetCore.Mvc;
using ChaidezMotorCompanyApi.Models;

namespace ChaidezMotorCompanyApi.Controllers.v1;

[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly ILogger<CarsController> _logger;

    public CarsController(ILogger<CarsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Car> Get()
    {
        List<Car> carList = new();
        carList.Add(new Car { Name = "CRS" });
        carList.Add(new Car { Name = "DRG" });

        return carList;
    }
}
