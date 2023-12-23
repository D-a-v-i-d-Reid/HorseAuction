//using System.ComponentModel.DataAnnotations.Schema;

//namespace HorseAuction
//{
//    public class AuctionHouse
//    {
//        public int AuctionId { get; set; }

//        public string HorseRegisterdName { get; set; }
        
//        public decimal StartingBid { get; set; }
//        public decimal CurrentBid { get; set; }
//        public decimal EndingBid { get; set; }

//        // Columns for seller and buyer usernames
//       // public string SellerUsername { get; set; }
//        //public string BuyerUsername { get; set; }

//        public DateTime AuctionEndDate { get; set; }

//        //[ForeignKey("SellerUsername")]
//        //public User Seller {  get; set; }

//        //[ForeignKey("BuyerUsername")]
//        //public User Buyer { get; set; }


//        [ForeignKey("HorseId")]
//        public Guid HorseId { get; set; } = Guid.NewGuid();
//        [ForeignKey("UserId")]
//        public Guid UserId { get; set; }

//        public Horse Horse { get; set; }
//        public User User { get; set; }
//    }
//}
