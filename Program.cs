using System.ComponentModel.Design;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;

namespace HorseAuction
{
    public class Program
    {
        static void Main(string[] args)
        {
            var logFilePath = Path.Combine(AppContext.BaseDirectory, "AuctionLogFile.txt");

            Console.WriteLine($"Log Directory: {Path.GetDirectoryName(logFilePath)}");
            Console.WriteLine();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog();
            });

            // Create a logger specifically for UserRegistrationService
            var userRegistrationLogger = loggerFactory.CreateLogger<UserRegistrationService>();

            // Create a logger specifically for AuctionDbContext
            var auctionDbContextLogger = loggerFactory.CreateLogger<AuctionDbContext>();


            //var logger = loggerFactory.CreateLogger<AuctionDbContext>();

            var optionsBuilder = new DbContextOptionsBuilder<AuctionDbContext>();
            optionsBuilder.UseSqlite("Data Source=AuctionData.db");
            var options = optionsBuilder.Options;

            var dbContext = new AuctionDbContext(options, auctionDbContextLogger);
            var auction = new Auction(dbContext);

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
                    //Create an instance of the UserRegistration Service
                    var userRegistrationService = new UserRegistrationService(dbContext, userRegistrationLogger);

                    // User Registration
                    userRegistrationService.RegisterUser();
                    break;

                case "2":
                    //Create an instance of AuthenticationService passsing AuctionDbContext
                    var authenticationService = new AuthenticationService(dbContext);

                    // Add Horse Registration Service
                    var horseRegistraitonLogger = loggerFactory.CreateLogger<HorseRegistrationService>();
                    var horseRegistrationService = new HorseRegistrationService(dbContext, horseRegistraitonLogger, new AuthenticationService(dbContext));
                    break;

                case "3":
                    //Add method for Auction Block 
                    break;

                case "4":
                    //Add Method for transction history
                    break;

                default:
                    Console.WriteLine("Invalid Choice. Please enter a valid option (1-4).");
                    break;
            }
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



