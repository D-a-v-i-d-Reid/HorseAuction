using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HorseAuction
{
    public class HorseManagementService
    {
        private readonly AuctionDbContext dbContext;
        private readonly ILogger<HorseManagementService> logger;
        private readonly AuthenticationService authenticationService;

        public HorseManagementService(
            AuctionDbContext dbContext,
            ILogger<HorseManagementService> logger,
            AuthenticationService authentication)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.authenticationService = authentication;
        }

        public void RunHorseManagementMenu()
        {
            Console.Clear();
            Console.WriteLine("Lazy R Quarter Horse Sale");
            Console.WriteLine("Horse Registration Management");

            bool continueManagement = true;

            while (continueManagement)
            {
                Console.WriteLine("1. Enter a Horse in Sale");
                Console.WriteLine("2. Edit a Horse in Sale");
                Console.WriteLine("3. Delete a Horse from Sale");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            EnterHorseInSale();
                            break;
                        case 2:
                            EditHorseInSale();
                            break;
                        case 3:
                            DeleteHorseFromSale();
                            break;
                        case 4:
                            Console.WriteLine("Exiting Horse Registration Management. Goodbye!");
                            continueManagement = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number corresponding to the menu options.");
                }
            }

        }
        public void EnterHorseInSale()
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
                        horse = MapToDomainModel(horseInput, authenticatedUser.UserName);

                        try
                        {
                            dbContext.Horses.Add(horse);
                            dbContext.SaveChanges();

                            Console.WriteLine($"Horse registered successfully with HorseID: {horse.HorseId}");
                            Console.WriteLine($"Horse Name: {horse.RegisteredName}");
                            Console.WriteLine($"Horse Age: {horse.Age}");
                            Console.WriteLine($"Horse Sex: {horse.Sex}");
                            Console.WriteLine($"Horse Color: {horse.Color}");
                            Console.WriteLine($"Horse Performance Type: {horse.PerformanceType}");
                            Console.WriteLine($"Horse Description: {horse.Description}");
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
        public void EditHorseInSale()
        {
            // Authenticate user before allowing horse registraiton
            var authenticatedUser = authenticationService.AuthenticateUser();

            if (authenticatedUser == null)
            {
                Console.WriteLine("Authentication Failed. Exiting Horse Registraiton.");
                return;
            }

            Console.Write("Enter Horse Registered Name To Edit: ");
            string registeredName = Console.ReadLine();

            var horse = dbContext.Horses.FirstOrDefault(h => h.RegisteredName == registeredName);

            if (horse != null)
            {
                Console.WriteLine("Select the property to edit:");
                Console.WriteLine("1. Horse Registered Name");
                Console.WriteLine("2. Horse Age");
                Console.WriteLine("3. Horse Sex");
                Console.WriteLine("4. Horse Color");
                Console.WriteLine("5. Horse Description");
                Console.WriteLine("6. Horse Performance Type");
                Console.WriteLine("7. Cancel");

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
                            Console.WriteLine("Edit Horse Sex: ");
                            horse.Sex = Console.ReadLine();
                            break;
                        case 4:
                            Console.Write("Enter new Horse Color: ");
                            horse.Color = Console.ReadLine();
                            break;
                        case 5:
                            Console.Write("Enter new Horse Description: ");
                            horse.Description = Console.ReadLine();
                            break;
                        case 6:
                            Console.Write("Enter new Horse Performance Type: ");
                            horse.PerformanceType = Console.ReadLine();
                            break;
                        case 7:
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
        public void DeleteHorseFromSale()
        {
            // Authenticate user before allowing horse registraiton
            var authenticatedUser = authenticationService.AuthenticateUser();

            if (authenticatedUser == null)
            {
                Console.WriteLine("Authentication Failed. Exiting Horse Registraiton.");
                return;
            }
            bool horseFound = false;

            while (!horseFound)
            {
                Console.Write("Enter Horse Registered Name to delete: ");
                string registeredName = Console.ReadLine();

                if (registeredName.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Deletion canceled. Exiting...");
                    return;
                }

                registeredName = registeredName.Trim();

                var horse = dbContext.Horses
                    .AsEnumerable()
                    .FirstOrDefault(h => h.RegisteredName.Trim().Equals(registeredName.Trim(), StringComparison.OrdinalIgnoreCase)); 

                if (horse != null)
                {
                    horseFound = true;
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
        private bool HorseNameIsPersisted(string horseName)
        {
            try
            {
                return dbContext.Horses.Any(h => h.RegisteredName.ToLower() == horseName.ToLower());
            }
            catch (Exception ex)
            {
                logger.LogError($"Error checking if horse name is persisted: {ex.Message}");
                return false;
            }
        }
        private void LogErrorandConsole(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            logger.LogError(errorMessage);
        }
        private void LogInfoAndConsole(string infoMessage)
        {
            Console.WriteLine(infoMessage);
            logger.LogInformation(infoMessage);
        }
        private void LogWarningandConsole(string warningMessage)
        {
            Console.WriteLine(warningMessage);
            logger.LogWarning(warningMessage);
        }
        private bool ValidateHorseInput(HorseInputModel horseInput, List<ValidationResult> validationResults)
        {
            return Validator.TryValidateObject(horseInput, new ValidationContext(horseInput), validationResults, validateAllProperties: true);
        }
        private Horse MapToDomainModel(HorseInputModel horseInput, string sellerUsername)
        {
            return new Horse
            {
                HorseId = Guid.NewGuid(),
                RegisteredName = horseInput.RegisteredName,
                Age = horseInput.Age,
                Sex = horseInput.Sex,
                Color = horseInput.Color,
                Description = horseInput.Description,
                PerformanceType = horseInput.PerformanceType,
                Seller = sellerUsername
            };
        }
        private HorseInputModel GetHorseInput(string registeredName)
        {
            HorseInputModel horseInput = null;

            bool isValidInput = false;

            do
            {
                Console.Write("Enter Horse Age: ");
                if (!int.TryParse(Console.ReadLine(), out int age))
                {
                    Console.WriteLine("Invalid input for Horse Age. Please enter a valid number.");
                    continue; 
                }
                Console.Write("Enter Horse Sex:");
                string sex = Console.ReadLine();

                Console.Write("Enter Horse Color: ");
                string color = Console.ReadLine();

                Console.Write("Enter Horse Description: ");
                string description = Console.ReadLine();

                horseInput = new HorseInputModel
                {
                    RegisteredName = registeredName,
                    Age = age,
                    Sex = sex,
                    Color = color,
                    Description = description,
                };

                bool invalidPerformanceType;

                do
                {

                    Console.WriteLine("Performance Type must entered as WesternPleasure, HunterUnderSaddle or Reining to be valid");
                    Console.WriteLine("Enter Horse Performance Type: ");

                    horseInput.PerformanceType = Console.ReadLine();

                    // Validate the performance type 
                    invalidPerformanceType = !Enum.TryParse<HorsePerformanceType>(horseInput.PerformanceType, true, out _);

                    if (invalidPerformanceType)
                    {
                        Console.WriteLine("Invalid performance type. Please enter a valid performance type.");
                    }
                } while (invalidPerformanceType);
                
                isValidInput = true;

            }while (!isValidInput);
            return horseInput;
        }
    }
}













