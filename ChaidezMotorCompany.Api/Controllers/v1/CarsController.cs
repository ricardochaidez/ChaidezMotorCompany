using Microsoft.AspNetCore.Mvc;
using ChaidezMotorCompany.Domain;
using AutoMapper;

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
        _mapper = mapper;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<CarViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Models.Car> cars = await _carDomain.GetCars(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<CarViewModel>>(cars));
    }
}