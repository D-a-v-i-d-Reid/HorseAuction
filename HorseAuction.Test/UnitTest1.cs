using Moq;
using Xunit;



namespace HorseAuction.Test
{
    public class AuctionTests
    {
        [Fact]
        public void DisplayHorses_ShouldNotThrowException()
        {
            // Arrange
            var dbContext = new Mock<AuctionDbContext>();
            var auction = new Auction(dbContext.Object);

            // Act 
            Action action = () => auction.DisplayHorses();

            //Assert
            Assert.DoesNotThrow(action);
        }
        [Fact]
        public void ViewHorseDetails_WithValidId_ShouldNotThrowException()
        {
            // Arrange
            var dbContext = new Mock<AuctionDbContext>();
            var auction = new Auction(dbContext.Object);

            // Act
            Action act = () => auction.ViewHorseDetails(1);

            // Assert
            Assert.DoesNotThrow(action);

        }
        [Fact]
        public void PlaceBid_WithValidInput_ShouldNotThrowException()
        {
            // Arrange
            var dbContext = new Mock<AuctionDbContext>();
            var auction = new Auction(dbContext.Object);

            // Act
            Action act = () => auction.PlaceBid(1, "TestBidder", 100);

            // Assert
            Assert.DoesNotThrow(act);
        }

    }
}