using HorseAuction;
using System;
using System.Collections.Generic;

public class Horse
{
    public Guid HorseId { get; set; } = Guid.NewGuid();
    public string RegisteredName { get; set; } = string.Empty;
    public int Age {  get; set; }
    public string Sex { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PerformanceType { get; set; } = string.Empty;
    public string Seller {  get; set; } = string.Empty;
    public Horse() 
    {
     HorseId = Guid.NewGuid();
    }
   // public List<AuctionHouse> AuctionHouses { get; set; }
}
