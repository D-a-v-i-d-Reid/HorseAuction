//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Metadata.Ecma335;
//using System.Text;
//using System.Threading.Tasks;

//namespace HorseAuction
//{
//    public class AuctionHouseLogic
//    {
//        private readonly AuctionDbContext _dbContext;

//        public AuctionHouseLogic(AuctionDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public void CreateAuction(User sellerUser, User buyerUser)
//        {
//            var auction = new AuctionHouse
//            {
//                SellerUsername = sellerUser.UserName,
//                BuyerUsername = buyerUser.UserName
//            };
//            _dbContext.AuctionHouses.Add(auction);
//            _dbContext.SaveChanges();

//            Console.WriteLine("Auction created successfully!");

//        }



//    }
//}
