using System;

public class Bid : Users
{
    public int BidId { get; set; }
    public int HorseId { get; set; }

    public decimal Amount { get; set; }
    public string Bidder { get; set; } = string.Empty;
    public string? BidderName { get; set; }
    public Horse Horse { get; set; }


}
