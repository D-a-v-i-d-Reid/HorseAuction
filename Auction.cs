using System;
using System.Linq;
using HorseAuction;
using Microsoft.EntityFrameworkCore;

public class Auction 
{
    private readonly AuctionDbContext _dbContext;


    public Auction(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
        //_dbContext.Database.EnsureCreated();

    }
    public void DisplayHorses()
    {
        var horses = _dbContext.Horses.ToList();
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

    public void Placebid(int horseId, string bidderName, decimal bidAmount)
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
                // Create Bid
                var bid = new Bid { HorseId = horseId, Amount = bidAmount, BidderName = bidderName };
                _dbContext.Bids.Add(bid);
                                
                //save changes and commit
                _dbContext.SaveChanges();
                transaction.Commit();

                Console.WriteLine($"Bid placed successfully on {selectedHorse.HorseName} for {bidAmount:C} by {bidderName}.");

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






