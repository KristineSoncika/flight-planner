using FlightPlanner.Models;

namespace FlightPlanner.Repositories;

public static class FlightRepository
{
    private static readonly object _flightsLock = new();
    private static readonly List<Flight> _flights = new();
    private static int _id;

    public static Flight AddFlight(AddFlight request)
    {
        lock (_flightsLock)
        {
            var flight = new Flight
            {
                Id = ++_id,
                ArrivalTime = request.ArrivalTime,
                Carrier = request.Carrier,
                DepartureTime = request.DepartureTime,
                From = request.From,
                To = request.To
            };
            _flights.Add(flight);

            return flight;
        }
    }

    public static Flight GetFlight(int id)
    {
        lock (_flightsLock)
        {
            return _flights.SingleOrDefault(flight => flight.Id == id);
        }
    }

    public static void DeleteFlight(int id)
    {
        var flight = GetFlight(id);
        
        lock (_flightsLock)
        {
            if (flight != null)
            {
                _flights.Remove(flight);
            }
        }
    }

    public static bool FlightAlreadyExists(AddFlight request)
    {
        lock (_flightsLock)
        {
            return _flights.Any(flight => flight.ArrivalTime == request.ArrivalTime && 
                                          flight.DepartureTime == request.DepartureTime && 
                                          string.Equals(flight.From.AirportCode, request.From.AirportCode, 
                                              StringComparison.CurrentCultureIgnoreCase) && 
                                          string.Equals(flight.To.AirportCode,request.To.AirportCode, 
                                              StringComparison.CurrentCultureIgnoreCase));
        }
    }

    public static void Clear()
    { 
        _flights.Clear();
        _id = 1;
    }

    public static List<Airport> FindAirport(string phrase)
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
                return airports.ToList();
            }
    
            if (flight.To.City.ToLower().Contains(formattedPhrase) ||
                flight.To.Country.ToLower().Contains(formattedPhrase) ||
                flight.To.AirportCode.ToLower().Contains(formattedPhrase))
            {
                airports.Add(flight.To);
                return airports.ToList();
            }
        }
        
        return airports.ToList();
    }

    public static PageResult SearchFlight(FlightSearch search)
    {
        var result = new PageResult();
        var flights = _flights.Where(flight =>
            flight.DepartureTime.Contains(search.DepartureDate) &&
            flight.From.AirportCode.ToLower().Contains(search.From.ToLower().Trim()) &&
            flight.To.AirportCode.ToLower().Contains(search.To.ToLower().Trim())).ToList();
        result.Items = flights;
        result.TotalItems = flights.Count;
        return result;
    }
}