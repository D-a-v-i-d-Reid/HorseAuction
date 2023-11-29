using System;
using System.ComponentModel.Design;

public class Auction
{
    private readonly AuctionDbContext _dbContext;

    public Auction(AuctionDbContext _dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();

    }

    public void DisplayHorses()
    {
        var horses = _dbContext.Horses.ToList()
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
            Console.WriteLine($"Details for {selectedHorse.HorseName} (ID: {selectedHorse.HorseId}):");
            Console.WriteLine($"Starting Bid: {selectedHorse.StartingBid:C}");
        }
        else
        {
            Console.WriteLine("Horse not found.");
        }
    }

    public void Placebid(int horseId, string bidderName, decimal bidAmount)
    {
        var selectedHorse = _dbContext.Horses.Find(horseId);

        if (selectedHorse != null)
        {
            var currentHighestBid = _dbContext.Bids
                .Where(b => b.HorseId == horseId)
                .OrderbyDescending(b => b.Amount)
                .FirstOrDefault();

            if (currentHighestBid == null) || bidAmount > currentHighestBid) 

            {
                var bid = new Bid { horseId = horseId, Amount = bidAmount, bidderName = bidderName };
                _dbContext.Bids.Add(bid);
                _dbContext.SaveChanges();
                Console.WriteLine($"Bid place successfully by {bidderName} on {selectedHorse.HorseName}.");
            }
            else
            {
                Console.WriteLine($"Bid amount must be higher than the current highest bid ({currentHighestBid.Amount:C}).");
            }
        }
        else
        {
            Console.WriteLine("Horse not found.");
        }
    }
}






