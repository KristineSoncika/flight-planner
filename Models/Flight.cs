namespace FlightPlanner.Models;

public class Flight
{
    public int Id { get; set; }
    public Airport  From { get; set; }
    public Airport  To { get; set; }
    public string Carrier { get; set; }
    public string DepartureTime { get; set; }
    public string ArrivalTime { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Flight flight)
        {
            return false;
        }

        return From.Equals(flight.From) &&
               To.Equals(flight.To) &&
               Carrier == flight.Carrier &&
               DepartureTime == flight.DepartureTime &&
               ArrivalTime == flight.ArrivalTime;
    }
}