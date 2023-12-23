using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace HorseAuction
{
    public class Program
    {
        static void Main(string[] args)
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

            // Create instances of services with ILogger injected
            var userRegistrationService = new UserRegistrationService(serviceProvider.GetRequiredService<AuctionDbContext>(), serviceProvider.GetRequiredService<ILogger<UserRegistrationService>>());
            var authenticationService = new AuthenticationService(serviceProvider.GetRequiredService<AuctionDbContext>(), serviceProvider.GetRequiredService<ILogger<AuthenticationService>>());
            var horseRegistrationService = new HorseRegistrationService(serviceProvider.GetRequiredService<AuctionDbContext>(), serviceProvider.GetRequiredService<ILogger<HorseRegistrationService>>(), authenticationService);



            Console.WriteLine("\n\t\tWelcome to the Lazy R Ranch Quarter Horse Auction\n");
            Console.WriteLine("First-time users must register with the Auction House to proceed.");
            Console.WriteLine("Select a unique username; it is required to access all functions in the Auction House.");
            Console.WriteLine("Once registered, you can enter a horse for sale, place a bid on a horse currently on the Auction Block, or view a history of previous transactions.\n");

            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Create a new account");
            Console.WriteLine("2. Register a horse for the auction");
            Console.WriteLine("3. Enter the auction house");
            Console.WriteLine("4. View previous transactions");

            Console.Write("Enter your choice (1-4): ");
            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    // User Registration
                    userRegistrationService.RegisterUser();
                    break;

                case "2":
                    //Horse Registration
                    horseRegistrationService.RegisterHorse();
                    break;


                    //    case "3":


                    //            var auctionLogic = new AuctionHouseLogic(dbContext);
                    //            auctionLogic.CreateAuction(sellerUser, buyerUser);

                    //        break;

                    //    case "4":
                    //        //Add Method for transction history
                    //        break;

                    //    default:
                    //        Console.WriteLine("Invalid Choice. Please enter a valid option (1-4).");
                    //        break;
                    //}
                    Console.WriteLine("Thank you for using the Lazy R Ranch Quarter Horse Auction. Goodbye!");





                    ////Once registered...Proceed to auction menu
                    //bool exitProgram = false;


                    //while (!exitProgram)
                    //{
                    //    Console.WriteLine();
                    //    Console.WriteLine("1. View Horses");
                    //    Console.WriteLine("2. View Horse Details");
                    //    Console.WriteLine("3. Place Bid");
                    //    Console.WriteLine("4. Exit");
                    //    Console.WriteLine("Enter Your Choice:  ");

                    //    if (int.TryParse(Console.ReadLine(), out int choice))
                    //    {


                    //        switch (choice)
                    //        {
                    //            case 1:
                    //                auction.DisplayHorses();
                    //                break;

                    //            case 2:
                    //                Console.Write("Enter the Horse ID to view details:  ");
                    //                if (int.TryParse(Console.ReadLine(), out int horseId))
                    //                {
                    //                    auction.ViewHorseDetails(horseId);

                    //                }
                    //                else
                    //                {
                    //                    Console.WriteLine("Invalid input. Please enter a number.");
                    //                }
                    //                break;

                    //            case 3:
                    //                Console.Write("Enter the HorseID to Place a Bid:  ");
                    //                if (int.TryParse(Console.ReadLine(), out horseId))
                    //                {
                    //                    string bidderName = Auction.GetBidderUsername();
                    //                    int bidderId = auction.RegisterBidder(bidderName);


                    //                    Console.Write("Enter the Bid Amount ");
                    //                    if (decimal.TryParse(Console.ReadLine(), out decimal bidAmount))
                    //                    {
                    //                        auction.Placebid(horseId, bidAmount, bidderId, bidderName);
                    //                    }
                    //                    else
                    //                    {
                    //                        Console.WriteLine("Invalid bid amount. Please enter a valid number.");
                    //                    }


                    //                }
                    //                else
                    //                {
                    //                    Console.WriteLine("Invalid Input. Please enter a number.");
                    //                }
                    //                break;
                    //            case 4:
                    //                exitProgram = true;
                    //                Console.WriteLine("Exiting the program.");
                    //                break;
                    //            default:
                    //                Console.WriteLine("Invalid Choice. Please enter a number between 1 and 4.");
                    //                break;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Invalid Input. Please enter a number.");
                    //    }
                    //    Console.WriteLine();
                    //}
            }
        }
    }
}




