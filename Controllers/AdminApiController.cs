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
    public IActionResult PutFlights(Flight flight)
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

        flight = FlightRepository.AddFlight(flight);
        return Created( "", flight);
    }
    
    [Route("{id:int}")]
    [HttpDelete]
    public IActionResult DeleteFlights(int id)
    {
        var flight = FlightRepository.GetFlight(id);

        if (flight == null)
        {
            return Ok();
        }
        
        flight = FlightRepository.DeleteFlight(flight);
        
        if (flight == null)
        {
            return NotFound();
        }

        return Ok(flight);
    }
}