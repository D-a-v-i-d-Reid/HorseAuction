using System;
using System.Linq;
using HorseAuction;
using Microsoft.EntityFrameworkCore;

public class Auction 
{
    private readonly AuctionDbContext _dbContext;
    private static readonly Random random = new Random();

    public Auction(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.Migrate();

    }
    public int RegisterBidder(string username)
    {
        Guid bidderGuid = Guid.NewGuid();
        int bidderId = BitConverter.ToInt32(bidderGuid.ToByteArray(), 0);

        while (_dbContext.Bidders.Any(b => b.BidderId == bidderId))
        {
            bidderGuid = Guid.NewGuid();
            bidderId = BitConverter.ToInt32(bidderGuid.ToByteArray(), 0);
        }

        var bidder = new Bidder { BidderId = bidderId, BidderName = username };
        _dbContext.Bidders.Add(bidder);
        _dbContext.SaveChanges();

        return bidderId;

            }
    public static string GetBidderUsername()
    {
        Console.Write("Enter Your Username: ");
        return Console.ReadLine();

    }
    public void DisplayHorses()
    {
        var horses = _dbContext.Horses.AsNoTracking().ToList();
        Console.WriteLine("List of Horses:");
        foreach (var horse in horses)
        {
            Console.WriteLine($"{horse.HorseId}. {horse.HorseName} (Starting Bid: {horse.StartingBid:C})");
        }
    }

    public void ViewHorseDetails(int horseId)
    {
        var selectedHorse = _dbContext.Horses.Find(horseId);
        if (selectedHorse != null)
        {
            Console.WriteLine($"Details for {selectedHorse.HorseName} (ID: {selectedHorse.HorseId}) {selectedHorse.Description}");
            Console.WriteLine($"Starting Bid: {selectedHorse.StartingBid:C}");
        }
        else
        {
            Console.WriteLine("Horse not found.");
        }
    }

    public void Placebid(int horseId, decimal bidAmount, int bidderId, string bidderName)
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                
                var selectedHorse = _dbContext.Horses.Find(horseId);
                if (selectedHorse == null)
                {
                    Console.WriteLine("Horse not found.");
                    return;
                }
                //Check Bid Amount Valid
                if (bidAmount <= 0)
                {
                    Console.WriteLine("Invalid bid amount. Please enter a valid number.");
                    return;
                }
                //Create a new bidder or get an existing one by name 
                var bidder = _dbContext.Bidders.FirstOrDefault(b => b.BidderName == bidderName);
                if (bidder == null)
                {
                    Console.WriteLine("Bidder not found.");
                    return;
                }

                {
                    // Create Bid
                    var bid = new Bid
                    {
                        HorseId = horseId,
                        Amount = bidAmount,
                        BidderId = bidder.BidderId

                    };

                    _dbContext.Bids.Add(bid);
                    _dbContext.SaveChanges();
                    transaction.Commit();

                    Console.WriteLine($"Bid placed successfully on {selectedHorse.HorseName} for {bidAmount:C} by {bidderName}.");
                }

            }
            catch (DbUpdateException ex) 
            {
                Console.WriteLine($"Error placing bid: {ex.Message}");
                transaction.Rollback();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error placing bid: {ex.Message}");
                transaction.Rollback();
            }


        }
    }
}






