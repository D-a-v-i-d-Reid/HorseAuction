using System;
using System.Collections.Generic;

namespace HorseAuction
{
    public class Bidder
    {
        public int BidderId { get; set; }
        public string BidderName { get; set; } = string.Empty;

        public List<Bid> Bids { get; set; } = new List<Bid>();
    }
}