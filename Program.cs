using System.ComponentModel.Design;

namespace HorseAuction
{
    public class Program
    {
        static void Main(string[] args)
        {

            using (var dbContext = new AuctionDbContext())
            {
                Auction auction = new(dbContext);
                               
                {                                       
                                                     
                                              
                    
                    bool exitProgram = false;

                    while (!exitProgram)
                    {
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
                                        Console.Write("Enter Your Username:  ");
                                        string? bidderName = Console.ReadLine();

                                        Console.Write("Enter the Bid Amount ");
                                        if (decimal.TryParse(Console.ReadLine(), out decimal bidAmount))
                                        {
                                            auction.Placebid(horseId, bidderName, bidAmount);
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
    }
}


