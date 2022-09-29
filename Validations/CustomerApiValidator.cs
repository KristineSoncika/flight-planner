using FlightPlanner.Models;

namespace FlightPlanner.Validations;

public static class CustomerApiValidator
{
    public static bool HasInvalidValues(FlightSearch flightSearch)
    {
        return flightSearch.From == null ||
               flightSearch.To == null ||
               flightSearch.DepartureDate == null;
    }

    public static bool IsSameAirport(FlightSearch flightSearch)
    {
        return flightSearch.From == flightSearch.To;
    }
}