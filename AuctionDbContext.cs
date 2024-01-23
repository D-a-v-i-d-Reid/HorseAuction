using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace HorseAuction
{
    public class AuctionDbContext : DbContext
    {
        private readonly ILogger<AuctionDbContext>? _logger;

        public AuctionDbContext(DbContextOptions<AuctionDbContext> options, ILogger<AuctionDbContext>? logger = null)
            : base(options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public DbSet<Horse> Horses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany(user => user.Horses)
                .WithOne(horse => horse.User)
                .HasForeignKey(horse => horse.UserId);

            modelBuilder.Entity<Horse>()
                    .HasOne(horse => horse.User)
                    .WithMany(user => user.Horses)
                    .HasForeignKey(horse => horse.UserId);

            modelBuilder.Entity<Horse>()
                    .HasMany(horse => horse.Bids)
                    .WithOne(bid => bid.Horse)
                    .HasForeignKey(bid => bid.HorseId);

            modelBuilder.Entity<Bid>()
                    .HasOne(bid => bid.Buyer)
                    .WithMany(user => user.Bids)
                    .HasForeignKey(bid => bid.UserId);
        }
    }
}


