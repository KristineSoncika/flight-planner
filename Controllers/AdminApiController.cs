using FlightPlanner.Models;
using FlightPlanner.Repositories;
using FlightPlanner.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("admin-api/flights")]
[ApiController, Authorize]
public class AdminApiController : ControllerBase
{
    [Route("{id:int}")]
    [HttpGet]
    public IActionResult GetFlights(int id)
    {
        var flight = FlightRepository.GetFlight(id);
        
        if (flight == null)
        {
            return NotFound();
        }

        return Ok(flight);
    }
    
    [HttpPut]
    public IActionResult AddFlight(AddFlight flight)
    {
        if (AdminApiValidator.HasInvalidValues(flight) || 
            AdminApiValidator.IsWrongDate(flight) || 
            AdminApiValidator.IsSameAirport(flight))
        {
            return  BadRequest();
        }

        if (FlightRepository.FlightAlreadyExists(flight))
        {
            return Conflict();
        }
        
        return Created( "",  FlightRepository.AddFlight(flight));
    }
    
    [Route("{id:int}")]
    [HttpDelete]
    public IActionResult DeleteFlight(int id)
    {
        FlightRepository.DeleteFlight(id);

        return Ok();
    }
}