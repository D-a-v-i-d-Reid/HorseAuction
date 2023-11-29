using System;
using Microsoft.EntityFrameworkCore;

public class AuctionDbContext : DbContext
{
	public DbSet<Horse> Horses { get; set; }
	public DbSet<Bid> Bids { get; set; }
    public DbSet<Bidder> Bidders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=mydatabase.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Horse)
            .WithMany(h => h.Bids)
            .HasForeignKey(b => b.HorseId);

        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Bidder)
            .WithMany()
            .HasForeignKey(b =>b.BidderId);

    }
}
