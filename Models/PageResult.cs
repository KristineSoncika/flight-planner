namespace FlightPlanner.Models;

public class PageResult
{
    public int Page { get; set; }
    public int TotalItems { get; set; }
    public Flight[] Items { get; set; }

    public PageResult(int page, int totalItems, Flight[] items)
    {
        Page = page;
        TotalItems = totalItems;
        Items = items;
    }
}