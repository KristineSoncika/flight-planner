using FlightPlanner.Models;
using FlightPlanner.Repositories;
using FlightPlanner.Validations;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApiController : ControllerBase
{
    [Route("airports")]
    [HttpGet]
    public IActionResult GetAirport(string search)
    {
        var searchResult = FlightRepository.FindAirport(search);
        return Ok(searchResult);
    }
    
    [Route("flights/search")]
    [HttpPost]
    public IActionResult SearchFlight(FlightSearch flightSearch)
    {
        if (CustomerApiValidator.HasInvalidValues(flightSearch) ||
            CustomerApiValidator.IsSameAirport(flightSearch))
        {
            return BadRequest();
        }
        
        var result = FlightRepository.SearchFlight(flightSearch); 
        return Ok(result);
    }
    
    [Route("flights/{id:int}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = FlightRepository.GetFlight(id);
        
        if (flight == null)
        {
            return NotFound();
        }
        
        return Ok(flight);
    }
}