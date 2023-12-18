using Microsoft.AspNetCore.Mvc;
using ChaidezMotorCompany.Domain;
using System.Net.Mime;
using Microsoft.AspNetCore.Components.Forms.Mapping;

namespace ChaidezMotorCompany.Api.Controllers.v1;

[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly ILogger<CarsController> _logger;
    private readonly ICarDomain _carDomain;
    private readonly IMapper _mapper;

    public CarsController(ILogger<CarsController> logger, ICarDomain carDomain, IMapper mapper)
    {
        _logger = logger;
        _carDomain = carDomain;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof())]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
         var cars = await _carDomain.GetCars(cancellationToken);
         return Ok(cars);
    }
}
