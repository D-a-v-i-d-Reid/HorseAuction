using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Bid 
{
    public int BidId { get; set; }

    public decimal Amount { get; set; }
        
    [ForeignKey("Horse")]
    public int HorseId { get; set; }
    public Horse Horse { get; set; }

    [ForeignKey("Bidder")]
    public int BidderId { get; set; }
    public Bidder Bidder { get; set; }

}
