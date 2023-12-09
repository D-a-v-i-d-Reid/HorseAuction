using System.ComponentModel.Design;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
            var logger = loggerFactory.CreateLogger<AuctionDbContext>();

            var optionsBuilder = new DbContextOptionsBuilder<AuctionDbContext>();
            optionsBuilder.UseSqlite("Data Source=AuctionData.db");
            var options = optionsBuilder.Options;

            var dbContext = new AuctionDbContext(options, logger);
                                             
                     
            Auction auction = new(dbContext);

            bool exitProgram = false;

            Console.WriteLine("Lazy R Quarter Horse Auction");

            while (!exitProgram)
            {
                Console.WriteLine();
                Console.WriteLine("1. View Horses");
                Console.WriteLine("2. View Horse Details");
                Console.WriteLine("3. Place Bid");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter Your Choice:  ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {


                    switch (choice)
                    {
                        case 1:
                            auction.DisplayHorses();
                            break;

                        case 2:
                            Console.Write("Enter the Horse ID to view details:  ");
                            if (int.TryParse(Console.ReadLine(), out int horseId))
                            {
                                auction.ViewHorseDetails(horseId);

                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                            break;

                        case 3:
                            Console.Write("Enter the HorseID to Place a Bid:  ");
                            if (int.TryParse(Console.ReadLine(), out horseId))
                            {
                                string bidderName = Auction.GetBidderUsername();
                                int bidderId = auction.RegisterBidder(bidderName);
                                                                                               

                                Console.Write("Enter the Bid Amount ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal bidAmount))
                                {
                                    auction.Placebid(horseId, bidAmount, bidderId, bidderName);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid bid amount. Please enter a valid number.");
                                }


                            }
                            else
                            {
                                Console.WriteLine("Invalid Input. Please enter a number.");
                            }
                            break;
                        case 4:
                            exitProgram = true;
                            Console.WriteLine("Exiting the program.");
                            break;
                        default:
                            Console.WriteLine("Invalid Choice. Please enter a number between 1 and 4.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input. Please enter a number.");
                }
                Console.WriteLine();




            }


        }






    }
}



