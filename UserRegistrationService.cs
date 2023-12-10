using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseAuction
{
    public class UserRegistrationService
    {
        private readonly AuctionDbContext dbContext;
        private readonly ILogger<UserRegistrationService> logger;

        public UserRegistrationService(AuctionDbContext dbContext, ILogger<UserRegistrationService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public void RegisterUser()
        {
            var userInput = GetUserInput();

            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(userInput, new ValidationContext(userInput), validationResults, validateAllProperties: true))
            {
                foreach (var validationResult in validationResults)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }
                return;

            }

            User user = null;

            // Check if the username is already taken
            if (UserNameIsTaken(user.UserName))
            {
                Console.WriteLine("Username is alreay taken. Please choose a different username. ");
                return;
            }
            
            try
            {
                user = MapToDomainModel(userInput);
                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                Console.WriteLine($"User registered successfully with UserID: {user.UserID}");
                Console.WriteLine($"Username: {user.UserName}");
                Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                Console.WriteLine($"Address: {user.StreetAddress}, {user.City}, {user.State} {user.PostalCode}");
                Console.WriteLine($"Phone: {user.CellPhone}");
                Console.WriteLine($"Email: {user.UserEmail}");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"An error occurred while saving the user: {ex.Message}");
                logger.LogError($"Error in RegisterUser method: {ex.Message}");
            }

        }

        private UserInputModel GetUserInput()
        {
            Console.Write("Enter UserName: ");
            var userName = Console.ReadLine();

            Console.Write("Enter FirstName: ");
            var firstName = Console.ReadLine();

            Console.Write("Enter LastName: ");
            var lastName = Console.ReadLine();

            Console.Write("Enter StreetAddress: ");
            var streetAddress = Console.ReadLine();

            Console.Write("Enter City: ");
            var city = Console.ReadLine();

            Console.Write("Enter State: ");
            var state = Console.ReadLine();

            Console.Write("Enter PostalCode: ");
            var postalCode = Console.ReadLine();

            Console.Write("Enter CellPhone: ");
            var cellPhone = Console.ReadLine();

            Console.Write("Enter UserEmail: ");
            var userEmail = Console.ReadLine();

            return new UserInputModel
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                StreetAddress = streetAddress,
                City = city,
                State = state,
                PostalCode = postalCode,
                CellPhone = cellPhone,
                UserEmail = userEmail
            };
        }

        private bool ValidateUserInput(UserInputModel userInput, List<ValidationResult> validationResults)
        {
            return Validator.TryValidateObject(userInput, new ValidationContext(userInput), validationResults, validateAllProperties: true);
        }

        private User MapToDomainModel(UserInputModel userInput)
        {
            return new User
            {
                UserID = Guid.NewGuid(),
                UserName = userInput.UserName,
                FirstName = userInput.FirstName,
                LastName = userInput.LastName,
                StreetAddress = userInput.StreetAddress,
                City = userInput.City,
                State = userInput.State,
                PostalCode = userInput.PostalCode,
                CellPhone = userInput.CellPhone,
                UserEmail = userInput.UserEmail
            };
        }
        private bool UserNameIsTaken(string userName)
        {
            return dbContext.Users.Any(u => u.UserName == userName);
        }

    }
}

    

