using Moq;
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
                var dbContext = new Mock<AuctionDbContext>();
                var auction = new Auction(dbContext.Object);

                // Act
                 Action act= () => auction.DisplayHorses();

                // Assert
                AssertNoExceptionThrown(act);
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
                AssertNoExceptionThrown(act);
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
                AssertNoExceptionThrown(act);
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
    }
