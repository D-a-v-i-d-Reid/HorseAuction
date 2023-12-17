using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseAuction
{
    public class AuctionHouse
    {
        public int AuctionId { get; set; }
        public decimal StartingBid { get; set; }
        public decimal EndingBid { get; set; }
        public decimal CurrentBid {  get; set; }

        [ForeignKey("Horse")]
        public Guid HorseId { get; set; } = Guid.NewGuid();
        [ForeignKey("User")]
        public Guid UserID { get; set; }


        public Horse Horse { get; set; }
        public User User { get; set; }
    }
}
