using FlightPlanner.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("testing-api/clear")]
[ApiController]
public class TestingApiController : ControllerBase
{
    [HttpPost]
    public IActionResult Clear()
    {
        FlightRepository.Clear();
        return Ok();
    }
}