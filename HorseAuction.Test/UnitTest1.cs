using System.Collections.Generic;
using Xunit;
using HorseAuction;
using System;


namespace HorseAuction.Test
{
     
    
    public class AuctionTests
    {

        [Fact]
        public void DisplayHorses_ShouldNotThrowException()
        {
            // Arrange
            var fakeDbContext = new FakeAuctionDbContext();
            var auction = new Auction(fakeDbContext);

            // Act
            Action act = () => auction.DisplayHorses();

            // Assert
            AssertNoExceptionThrown(act);
        }

        [Fact]
        public void ViewHorseDetails_WithValidId_ShouldNotThrowException()
        {
            // Arrange
            var fakeDbContext = new FakeAuctionDbContext();
            var auction = new Auction(fakeDbContext);

            // Act
            Action act = () => auction.ViewHorseDetails(1);

            // Assert
            AssertNoExceptionThrown(act);
        }

        [Fact]
        public void PlaceBid_WithValidInput_ShouldNotThrowException()
        {
            // Arrange
            var fakeDbContext = new FakeAuctionDbContext();
            var auction = new Auction(fakeDbContext);

            // Act
            auction.PlaceBid(1, "TestBidder", 100);

            // Assert
            Assert.Equal(1, fakeDbContext.Bids.Count);
            Assert.Equal(1, fakeDbContext.Bids[0].HorseId);
            Assert.Equal("TestBidder", fakeDbContext.Bids[0].Bidder.BidderName);
            Assert.Equal(100, fakeDbContext.Bids[0].Amount);
        }

        private static void AssertNoExceptionThrown(Action action)
        {
            try
            {
                // Act
                action();
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(false, $"Method threw an exception: {ex}");
            }
        }
    }
    public class FakeAuctionDbContext : AuctionDbContext
    {
        public List<Bid> Bids { get; } = new List<Bid>();

        public void SaveBid(Bid bid)
        {
            Bids.Add(bid);
        }
    }
}
