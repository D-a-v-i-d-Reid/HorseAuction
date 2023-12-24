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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define your entity configurations here if needed
        }
    }
}
