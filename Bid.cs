using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace HorseAuction
{
    public class Bid
    {
        public Guid BidId { get; set; }
        public string RegisteredName { get; set; }
        public string BuyerUserName { get; set; }  // Foreign Key to the User table
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }
        public string SellerUserName { get; set; } // Sellers UserName

        // Navigation properties 
        public Horse Horse { get; set; }
        public User Buyer { get; set; }
        public Guid HorseId { get; set; }
        public Guid UserId { get; set; }    
        
    }
}