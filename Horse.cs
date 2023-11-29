using System;

public class Horse
{
    public int HorseId { get; set; }
    public string HorseName { get; set; } = string.Empty;
    public int Age {  get; set; }
    public string Color { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PerformanceType { get; set; } = string.Empty;
    public decimal StartingBid { get; set; }

    public List<Bid>? Bids { get; set; } 
}
