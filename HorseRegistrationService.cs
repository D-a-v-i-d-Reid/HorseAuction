using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseAuction
{
    public class HorseRegistrationService
    {
        private readonly AuctionDbContext dbContext;
        private readonly ILogger<HorseRegistrationService> logger;
        private readonly AuthenticationService authenticationService;

        public HorseRegistrationService(
            AuctionDbContext dbContext,
            ILogger<HorseRegistrationService> logger,
            AuthenticationService authentication)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.authenticationService = authentication;
        }
        public void RegisterHorse()
        {
            // Authenticate user before allowing horse registraiton
            var authenticatedUser = authenticationService.AuthenticateUser();

            if (authenticatedUser == null)
            {
                Console.WriteLine("Authentication Failed. Exiting Horse Registraiton.");
                return;
            }

            Horse horse = null;

            do
            {
                Console.WriteLine("Enter Horse Name: ");
                var horseName = Console.ReadLine();

                // Check if the horse name is already in the database
                if (HorseNameIsPersisted(horseName))
                {
                    Console.WriteLine("Horse name is already taken. Please choose a different name.");
                }
                else if (horseName.Length > 20)
                {
                    Console.WriteLine("Horse name must be 20 characters or less. Please enter a valid horse name.");
                }
                else
                {
                    var horseInput = GetHorseInput(horseName);

                    //Validate horse Input
                    var validationResults = new List<ValidationResult>();
                    if (ValidateHorseInput(horseInput, validationResults))
                    {
                        horse = MapToDomainModel(horseInput);

                        try
                        {
                            dbContext.Horses.Add(horse);
                            dbContext.SaveChanges();

                            Console.WriteLine($"Horse registered successfully with HorseID: {horse.HorseId}");
                            Console.WriteLine($"Horse Name: {horse.RegisteredName}");
                            Console.WriteLine($"Horse Age: {horse.Age}");
                            Console.WriteLine($"Horse Color: {horse.Color}");
                            Console.WriteLine($"Horse Performance Type: {horse.PerformanceType}");
                            Console.WriteLine($"Horse Description: {horse.Description}");

                            //Ask the user if they want to edit or delete the horse
                            Console.WriteLine("Do you want to edit or delete the horse?");
                            Console.WriteLine("1. Edit Horse");
                            Console.WriteLine("2. Delete Horse");
                            Console.WriteLine("3. Continue without editing/deleting");
                            Console.Write("Enter your choice: ");
                            int choice;
                            if (int.TryParse(Console.ReadLine(), out choice))
                            {
                                switch (choice)
                                {
                                    case 1:
                                        EditHorse();
                                        break;
                                    case 2:
                                        DeleteHorse();
                                        break;
                                    case 3:
                                        Console.WriteLine("Continuing without editing/deleting.");
                                        break;
                                    default:
                                        Console.WriteLine("Invalid choice. Continuing without editing/deleting.");
                                        break;
                                }
                            }
                        }
                        catch (DbUpdateException ex)
                        {
                            Console.WriteLine($"An error occurred while saving the horse: {ex.Message}");
                            logger.LogError($"Error in RegisterHorse method: {ex.Message}");
                        }
                    }
                    else
                    {
                        foreach (var validationResult in validationResults)
                        {
                            Console.WriteLine(validationResult.ErrorMessage);
                        }
                    }
                }
            } while (horse == null);
        }
        private bool HorseNameIsPersisted(string horseName)
        {
            return dbContext.Horses.Any(h.RegisteredName.ToLower() == horseName.ToLower());
        }
        private HorseInputModel GetHorseInput(string registeredName)
        {
            Console.Write("Enter Horse Age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Invalid input for Horse Age. Please enter a valid number.");
                return null;
            }
            Console.Write("Enter Horse Color: ");
            string color = Console.ReadLine();

            Console.Write("Enter Horse Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Horse Performance Type: ");
            string performanceType = Console.ReadLine();

            return new HorseInputModel
            {
                RegisteredName = registeredName,
                Age = age,
                Color = color,
                Description = description,
                PerformanceType = performanceType
            };
        }
        private bool ValidateHorseInput(HorseInputModel horseInput, List<ValidationResult> validationResults)
        {
            return Validator.TryValidateObject(horseInput, new ValidationContext(horseInput), validationResults, validateAllProperties: true);
        }
        private Horse MapToDomainModel(HorseInputModel horseInput)
        {
            return new Horse
            {
                HorseId = Guid.NewGuid(),
                RegisteredName = horseInput.RegisteredName,
                Age = horseInput.Age,
                Color = horseInput.Color,
                Description = horseInput.Description,
                PerformanceType = horseInput.PerformanceType
            };
        }
        public void EditHorse()
        {
            Console.Write("Enter Horse Registered Name To Edit: ");
            string registeredName = Console.ReadLine();

            var horse = dbContext.Horses.FirstOrDefault(h => h.RegisteredName.Equals(registeredName, StringComparison.OrdinalIgnoreCase));

            if (horse != null)
            {
                Console.WriteLine("Select the property to edit:");
                Console.WriteLine("1. Horse Registered Name");
                Console.WriteLine("2. Horse Age");
                Console.WriteLine("3. Horse Color");
                Console.WriteLine("4. Horse Description");
                Console.WriteLine("5. Horse Performance Type");
                Console.WriteLine("6. Cancel");

                Console.Write("Enter your choice: ");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter new Horse Registered Name; ");
                            horse.RegisteredName = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("Enter new Horse Age: ");
                            if (int.TryParse(Console.ReadLine(), out int age))
                            {
                                horse.Age = age;
                            }
                            break;
                        case 3:
                            Console.Write("Enter new Horse Color: ");
                            horse.Color = Console.ReadLine();
                            break;
                            case 4:
                            Console.Write("Enter new Horse Description: ");
                            horse.Description = Console.ReadLine();
                            break;
                            case 5:
                            Console.Write("Enter new Horse Performance Type: ");
                            horse.PerformanceType = Console.ReadLine();
                            break; 
                            case 6:
                            Console.WriteLine("Edit Canceled.");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Edit Canceled.");
                            return;
                    }
                    dbContext.SaveChanges();
                    Console.WriteLine($"Horse with registered name {registeredName} has been updated successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid choice. Edit Canceled.");
                }
            }
            else
            {
                Console.WriteLine($"Horse with registered name {registeredName} not found.");
            }
        }
        public void DeleteHorse()
        {
            Console.Write("Enter Horse Registered Name to delete: ");
            string registeredName = Console.ReadLine();

            var horse = dbContext.Horses.FirstOrDefault(h => h.RegisteredName.Equals(registeredName, StringComparison.OrdinalIgnoreCase));

            if (horse != null)
            {
                dbContext.Horses.Remove(horse);
                dbContext.SaveChanges();

                Console.WriteLine($"Horse with registered name {registeredName} has been deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Horse with registered name {registeredName} not found.");
            }
        }
    }
}
