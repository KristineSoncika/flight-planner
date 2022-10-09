using FlightPlanner.Models;
using static System.DateTime;

namespace FlightPlanner.Validations;

public static class AdminApiValidator
{
    public static bool HasInvalidValues(AddFlight flight)
    {
      return string.IsNullOrWhiteSpace(flight.From.Country) || 
             string.IsNullOrWhiteSpace(flight.From.City) || 
             string.IsNullOrWhiteSpace(flight.From.AirportCode) || 
             string.IsNullOrWhiteSpace(flight.To.Country) || 
             string.IsNullOrWhiteSpace(flight.To.City) || 
             string.IsNullOrWhiteSpace(flight.To.AirportCode) || 
             string.IsNullOrWhiteSpace(flight.Carrier) || 
             string.IsNullOrWhiteSpace(flight.DepartureTime) || 
             string.IsNullOrWhiteSpace(flight.ArrivalTime);
    }

    public static bool IsSameAirport(AddFlight flight)
    {
        return flight.From.AirportCode.ToUpper().Trim() == flight.To.AirportCode.ToUpper().Trim();
    }

    public static bool IsWrongDate(AddFlight flight)
    {
        TryParse(flight.ArrivalTime, out var arrivalTime);
        TryParse(flight.DepartureTime, out var departureTime);

        return arrivalTime <= departureTime;
    }
}