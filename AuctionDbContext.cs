using System;
using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
namespace HorseAuction;
public class AuctionDbContext : DbContext
{
    public DbSet<Horse> Horses { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Bidder> Bidders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try

        {
            optionsBuilder.UseSqlite("Data Source=AuctionData.db");
          //  optionsBuilder.EnableSensitiveDataLogging();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error configuring database: {ex.Message}");
        }
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        try
        {
            modelBuilder.Entity<Bid>()
            .HasOne(b => b.Horse)
            .WithMany()
            .HasForeignKey(b => b.HorseId);


            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Bidder)
                .WithMany()
                .HasForeignKey(b => b.BidderId);

            modelBuilder.Entity<Horse>().HasData(
               new Horse { HorseId = 1, HorseName = "Genetic", Age = 3, Color = "Red Dun", Description = "Easy Keeper. Fun to be Around", PerformanceType = "Western Pleasure", StartingBid = 3500m },
               new Horse { HorseId = 2, HorseName = "Rowdy", Age = 4, Color = "Bay with a snip", Description = "Barn sour and will kick you", PerformanceType = "Reining", StartingBid = 5000m },
               new Horse { HorseId = 3, HorseName = "Brownie", Age = 2, Color = "Brown", Description = "Very green, just started under saddle", PerformanceType = "Western Pleasure", StartingBid = 10000m }
               );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error on model building: {ex.Message}");
            
        }
    }


}










