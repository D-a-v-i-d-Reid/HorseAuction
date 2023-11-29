using System;

public class Bid 
{
    public int BidId { get; set; }
    public int HorseId { get; set; }
    public int BidderId { get; set; }

    public decimal Amount { get; set; }
    
    public string? BidderName { get; set; }
    public Horse Horse { get; set; }
    public Bidder Bidder { get; set; }

}
