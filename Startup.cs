using System;
using HorseAuction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

public class Startup
{
    // Other configuration methods...

    public void ConfigureServices(IServiceCollection services)
    {
        // Your existing service registrations...

        // Add logging for DbContext
        services.AddDbContext<AuctionDbContext>(options =>
        {
            // Other DbContext configurations...

            // Enable detailed logging
            options.EnableSensitiveDataLogging(); // This includes sensitive data in the log (use only in development)
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        });
    }
}
