using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HorseAuction
{
    public class Program
    {
        public static void Main()
        {
            //Specify configuration values
            var connectionString = "Data Source=AuctionData.db";
            var logFilePath = "AuctionLogFile.txt";

            // Set up logging
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, logFilePath), rollingInterval: RollingInterval.Day)
                    .CreateLogger();

            // Set up database options
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddSerilog())
                .AddDbContext<AuctionDbContext>(options => options.UseSqlite(connectionString))
                .BuildServiceProvider();

            // Apply Migrations
            using (var scope = serviceProvider.CreateScope())
            {
                var auctionDbContext = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

                if (auctionDbContext != null)
                {
                    auctionDbContext.Database.Migrate();
                }
            }
            // Create AuctionDbContext using the DbContextOptions
            using var dbContext = serviceProvider.GetRequiredService<AuctionDbContext>();


            // Create instances of services with ILogger injected
            var userRegistrationService = new UserRegistrationService(serviceProvider.GetRequiredService<AuctionDbContext>(), serviceProvider.GetRequiredService<ILogger<UserRegistrationService>>());
            var authenticationService = new AuthenticationService(serviceProvider.GetRequiredService<AuctionDbContext>(), serviceProvider.GetRequiredService<ILogger<AuthenticationService>>());
            var horseManagementService = new HorseManagementService(serviceProvider.GetRequiredService<AuctionDbContext>(), serviceProvider.GetRequiredService<ILogger<HorseManagementService>>(), authenticationService);



            Console.WriteLine("\n\t\tWelcome to the Lazy R Ranch Quarter Horse Auction\n");
            Console.WriteLine("First-time users must register with the Auction House to proceed.");
            Console.WriteLine("Create a unique username; Your User Name is required to access all functions in the Auction House.");
            Console.WriteLine("Once registered, you can enter a horse for sale, place a bid on a horse currently on the Auction Block, or view a history of previous transactions.\n");
            Console.WriteLine();
            Console.WriteLine("Press Enter to Proceed");
            Console.ReadLine();
            Console.Clear();
            bool continueProgram = true;

            while (continueProgram)
            {

                Console.WriteLine("\n\t\tWelcome to the Lazy R Ranch Quarter Horse Auction\n");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Create a new account");
                Console.WriteLine("2. Register or Edit horse for the auction");
                Console.WriteLine("3. Enter the auction house");
                Console.WriteLine("4. View previous transactions");
                Console.WriteLine("0. Exit Program");

                Console.Write("Enter your choice (0-4): ");
                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        // User Registration
                        Console.Clear();
                        userRegistrationService.RegisterUser();
                        return;

                    case "2":
                        //Horse Registration
                        horseManagementService.RunHorseManagementMenu();
                        break;

                    case "3":
                        // Add logic for entering the auction house
                        break;

                    case "4":
                        // Add Method for transction history
                        break;

                    case "0":
                        // Exit the program
                        Console.WriteLine();
                        Console.WriteLine("Thank you for using the Lazy R Ranch Quarter Horse Auction. Goodbye!");
                        continueProgram = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Choice. Please enter a valid option (0-4).");
                        break;
                }
                if (continueProgram)
                {
                    Console.WriteLine("Press Enter to return to the main menu");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }
    }
}


