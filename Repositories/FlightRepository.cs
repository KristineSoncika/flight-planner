using FlightPlanner.Models;

namespace FlightPlanner.Repositories;

public static class FlightRepository
{
    private static readonly object _flightsLock = new();
    private static readonly List<Flight> _flights = new();
    private static int _id = 1;

    public static Flight AddFlight(Flight flight)
    {
        lock (_flightsLock)
        {
            _flights.Add(flight);
        }
        
        flight.Id = _id++;
        return flight;
    }

    public static Flight GetFlight(int id)
    {
        lock (_flightsLock)
        {
            return _flights.FirstOrDefault(flight => flight.Id == id);
        }
    }

    public static Flight DeleteFlight(Flight flight)
    {
        lock (_flightsLock)
        {
            _flights.Remove(flight);
        }
        
        return flight;
    }

    public static bool FlightAlreadyExists(Flight flight)
    {
        lock (_flightsLock)
        {
            return _flights.Exists(f => f.Equals(flight));
        }
    }

    public static void Clear()
    { 
        _flights.Clear();
        _id = 1;
    }

    public static Airport[] FindAirport(string phrase)
    {

        var airports = new List<Airport>();
        var formattedPhrase = phrase.ToLower().Trim();
        
        foreach (var flight in _flights)
        {
            if (flight.From.City.ToLower().Contains(formattedPhrase) ||
                flight.From.Country.ToLower().Contains(formattedPhrase) ||
                flight.From.AirportCode.ToLower().Contains(formattedPhrase))
            {
                airports.Add(flight.From);
                return airports.ToArray();
            }
    
            if (flight.To.City.ToLower().Contains(formattedPhrase) ||
                flight.To.Country.ToLower().Contains(formattedPhrase) ||
                flight.To.AirportCode.ToLower().Contains(formattedPhrase))
            {
                airports.Add(flight.To);
                return airports.ToArray();
            }
        }
        
        return airports.ToArray();
    }

    public static PageResult SearchFlight(FlightSearch flightSearch)
    {
        var page = 0;
        var totalItems = 0;
        var items = new List<Flight>();
        
        foreach (var flight in _flights)
        {
            if (flight.From.AirportCode == flightSearch.From &&
                flight.To.AirportCode == flightSearch.To &&
                flight.DepartureTime[..10] == flightSearch.DepartureDate)
            {
                items.Add(flight);
                totalItems++;
                page++;
            }
        }

        var pageResult = new PageResult(page, totalItems, items.ToArray());
        return pageResult;
    }
}