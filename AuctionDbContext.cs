using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Provide the connection string directly
            var connectionString = "Data Source=AuctionData.db";
            optionsBuilder.UseSqlite(connectionString);

            _logger.LogInformation("Configuring DbContext with connection string: {ConnectionString}", connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define your entity configurations here if needed
        }
    }
}
