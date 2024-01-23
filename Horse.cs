using HorseAuction;
using System;
using System.Collections.Generic;

namespace HorseAuction
{
    public class Horse
    {
        public Guid HorseId { get; set; } = Guid.NewGuid();
        public string RegisteredName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PerformanceType { get; set; } = string.Empty;
        public string SellerUserName { get; set; } = string.Empty;
        
        public Guid UserId { get; set; }

        public User User { get; set; }

        public List<Bid> Bids { get; set; }
       // public User Owner {  get; set; }
        //public Guid OwnerId { get; set; }
        //public List<Auction> Auctions { get; set; }
    }
}