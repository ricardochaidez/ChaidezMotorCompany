using Microsoft.AspNetCore.Mvc;
using ChaidezMotorCompany.Domain.Models;

namespace ChaidezMotorCompany.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<CarsController> _logger;

    public CarsController(ILogger<CarsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Car> Get()
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
