using System;
using Microsoft.EntityFrameworkCore;

public class AuctionDbContext : DbContext
{
	public DbSet<Horse> Horses { get; set; }
	public DbSet<Bid> Bids { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=mydatabase.db");
    }

}
