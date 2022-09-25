using System.Globalization;
using FlightPlanner.Models;
using static System.DateTime;

namespace FlightPlanner.Validations;

public static class AdminApiValidator
{
    public static bool HasInvalidValues(Flight flight)
    {
        foreach(var flightProperty in flight.GetType().GetProperties())
        {
            if(flightProperty.PropertyType == typeof(string))
            {
                var value = (string)flightProperty.GetValue(flight);
                if(string.IsNullOrWhiteSpace(value))
                {
                    return true;
                }
            }
            
            var from = flight.From;
            var to = flight.To;
            
            if(flightProperty.PropertyType == typeof(Airport))
            {
                if (from.GetType().GetProperties().Select(fromProperty => (string)fromProperty.GetValue(from)).Any(string.IsNullOrWhiteSpace))
                {
                    return true;
                }

                if (to.GetType().GetProperties().Select(toProperty => (string)toProperty.GetValue(to)).Any(string.IsNullOrWhiteSpace))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    public static bool IsSameAirport(Flight flight)
    {
        return flight.From.City.ToUpper().Trim() == flight.To.City.ToUpper().Trim() &&
               flight.From.Country.ToUpper().Trim() == flight.To.Country.ToUpper().Trim() &&
               flight.From.AirportCode.ToUpper().Trim() == flight.To.AirportCode.ToUpper().Trim();
    }

    public static bool IsWrongDate(Flight flight)
    {
        TryParse(flight.ArrivalTime, out var arrivalTime);
        TryParse(flight.DepartureTime, out var departureTime);

        return arrivalTime <= departureTime;
    }
}