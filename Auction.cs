using System;
using System.ComponentModel.Design;

public class Auctiom
{
    private List<Horse> horses;
    private Dictionary<Horse, Bid> currentBids;
    private bool isAuctionRunning;


    public Auctiom(List<Horse> horses)
    {
        this.horses = horses;
        currentBids = new Dictionary<Horse, Bid>();
        isAuctionRunning = false;
    }

    public void StartAuction()
    {
        if (!isAuctionRunning)
        {
            Console.WriteLine("Auction Started!");
            isAuctionRunning = true;

            foreach (var horse in horses)
            {
                Console.WriteLine($"Auctioning {horse.HorseId} ({horse.HorseName})");
                Console.WriteLine($"Starting Bid: {horse.StartingBid:C}");
                Console.WriteLine();
                currentBids.Add(horse, new Bid { Amount = horse.StartingBid, Bidder = null });
            }
            Console.WriteLine("Bidding has begun! Place your bids.");
        }
        else
        {
            Console.WriteLine("The auction is already running.");
        }

    }
    public void PlaceBid(Users bidder, Horse horse, decimal bidAmount)
    {
        if (isAuctionRunning && horses.Contains(horse))
        {
            if (bidAmount > currentBids[horse].Amount)
            {
                currentBids[horse] = new Bid { Amount = bidAmount, Bidder = bidder.Name };
                Console.WriteLine($"{bidder.Name} placed a bid of {bidAmount:C} on {horse.HorseName}.");
            }
            else
            {
                Console.WriteLine($"Bid mustbe higher than the current highest bid ({currentBids[horse].Amount:C}).");
            }
        }
        else
        {
            Console.WriteLine("Auction is not running , or the horse is not in the auction.");
        }
    }
    public void EndAuction()
    {
        if (isAuctionRunning)
        {
            Console.WriteLine("Auction ended!");

            foreach (var horse in horses)
            {
                var winningBid = currentBids[horse];
                Console.WriteLine($"{horse.HorseName} {horse.HorseId}");
                Console.WriteLine($"Winning Bid: {winningBid.Amount:C} by {(winningBid.Bidder != null ? winningBid.Bidder : "No Bidder")}");
                Console.WriteLine();

            }
            isAuctionRunning = false;
        }
        else

        {
            Console.WriteLine("The auction is not currently running.");
        }



    }
}








