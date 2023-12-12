using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            User user = null;

            do
            {
                Console.Write("Enter UserName: ");
                var userName = Console.ReadLine();

                //Check if the username is already in the database
                if (UserNameIsPersisted(userName) || UserNameIsTakenInSession(userName))
                {
                    Console.WriteLine("UserName is already taken. Please choose a differnt username.");
                }
                else if (userName.Length < 3)
                {
                    Console.WriteLine("UserName must be at least 3 characters long. Please enter a valid username.");
                }
                else
                {
                    var userInput = GetUserInput(userName);


                    // Validate user input
                    var validationResults = new List<ValidationResult>();
                    if (ValidateUserInput(userInput, validationResults))
                    {
                        user = MapToDomainModel(userInput);

                        try
                        {
                            dbContext.Users.Add(user);
                            dbContext.SaveChanges();

                            Console.WriteLine($"User registered successfully with UserID: {user.UserID}");
                            Console.WriteLine($"Username: {user.UserName}");
                            Console.WriteLine($"Name: {user.FullName}");
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
                    else
                    {
                        foreach (var validationResult in validationResults)
                        {
                            Console.WriteLine(validationResult.ErrorMessage);
                        }
                    }
                }
            } while (user == null);
        }
        private bool UserNameIsPersisted(string userName)
        {
            return dbContext.Users.Any(u => u.UserName.ToLower() == userName.ToLower());
        }


        private bool UserNameIsTakenInSession(string username)
        {
            return dbContext.ChangeTracker.Entries<User>().Any(e => e.Entity.UserName.ToLower() == username.ToLower());
        }

        private UserInputModel GetUserInput(string userName)
        {
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
            string formattedPhoneNumber = FormatPhoneNumberForDisplay(cellPhone);


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
                CellPhone = formattedPhoneNumber,
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
                FirstName = CapitalizeEachWord(userInput.FirstName),
                LastName = CapitalizeEachWord(userInput.LastName),
                StreetAddress = CapitalizeEachWord(userInput.StreetAddress),
                FullName = CapitalizeEachWord(userInput.FirstName + " " + userInput.LastName),
                City = CapitalizeEachWord(userInput.City),
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

        private string FormatPhoneNumberForDisplay(string unformattedPhoneNumber)
        {
            string numericPhoneNumber = Regex.Replace(unformattedPhoneNumber, "[^0-9]", "");

            if (numericPhoneNumber.Length == 10)
            {
                return $"({numericPhoneNumber.Substring(0, 3)}) {numericPhoneNumber.Substring(3, 3)}-{numericPhoneNumber.Substring(6)}";
            }
            else
            {
                return "Invalid Phone Number";
            }
        }
        private string CapitalizeEachWord(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string[] words = input.Split(' ');
            for (int i =0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    char[] letters  = words[i].ToCharArray();
                    letters[0] = char.ToUpper(letters[0]);
                    words[i] = new string(letters);
                }
            }
            return string.Join(" ", words);
        } 



    }
}






