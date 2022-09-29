using System.Text.Json.Serialization;

namespace FlightPlanner.Models;

public class Airport
{
    public string Country { get; set; }
    public string City { get; set; }
    
    [JsonPropertyName("airport")]
    public string AirportCode { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not Airport airport)
        {
            return false;
        }

        return Country == airport.Country &&
               City == airport.City &&
               AirportCode == airport.AirportCode;
    }
}