using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HorseAuction
{
    public class UserRegistrationService
    {
        private readonly AuctionDbContext dbContext;
        private readonly ILogger<UserRegistrationService> logger;

        public UserRegistrationService(AuctionDbContext dbContext, ILogger<UserRegistrationService> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void RegisterUser()
        {
            User user = null;

            Console.WriteLine("A unique User Name must be created. It must be at least 3 letters and cannot contain any white space between words."); 
            Console.WriteLine();
            Console.WriteLine("The User Name is permanent and can not be changed. It will be needed to access all areas of the Auction House.");

            do
            {
                Console.WriteLine();
                Console.Write("Create a UserName: ");
                var userName = Console.ReadLine();

                //Check if the username is already in the database
                if (UserNameIsPersisted(userName) || UserNameIsTakenInSession(userName))
                {
                    LogWarningAndConsole("User Name is already taken. Please choose a different User Name.");
                }
                else if (userName.Length < 3)
                {
                    LogWarningAndConsole("User Name must be at least 3 characters long. Please enter a valid User Name.");
                }
                else if (ContainsWhiteSpace(userName))
                {
                    LogWarningAndConsole("User Name cannot contain whitespace between word. Please enter a valid User Name");
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

                            Console.WriteLine();
                            LogInfoAndConsole($"User registered successfully with UserID: {user.UserId}");
                            LogInfoAndConsole($"Username: {user.UserName}");
                            LogInfoAndConsole($"Name: {user.FullName}");
                            LogInfoAndConsole($"Address: {user.StreetAddress}, {user.City}, {user.State} {user.PostalCode}");
                            LogInfoAndConsole($"Phone: {user.CellPhone}");
                            LogInfoAndConsole($"Email: {user.UserEmail}");
                        }
                        catch (DbUpdateException ex)
                        {
                            LogErrorAndConsole($"An error occurred while saving the user: {ex.Message}");
                            
                        }
                    }
                    else
                    {
                        foreach (var validationResult in validationResults)
                        {
                            LogWarningAndConsole(validationResult.ErrorMessage);
                        }
                    }
                }
            } while (user == null);
        }
        private void LogErrorAndConsole(string errorMessage)
        {
            Console.Write(errorMessage);
            logger.LogError(errorMessage);
        }
        private void LogInfoAndConsole(string infoMessage)
        {
            Console.WriteLine(infoMessage);
            logger.LogInformation(infoMessage);
        }
        private void LogWarningAndConsole(string warningMessage)
        {
            Console.WriteLine(warningMessage);
            logger.LogWarning(warningMessage);
        }
        private bool UserNameIsPersisted(string userName)
        {
            string trimmedUserName = userName.Trim();
            return dbContext.Users.Any(u => u.UserName.Trim().ToLower() == trimmedUserName.ToLower());
        }


        private bool UserNameIsTakenInSession(string username)
        {
            string trimmedUsername = username.Trim();

            return dbContext.ChangeTracker.Entries<User>().Any(e => e.Entity.UserName.Trim().Equals(trimmedUsername, StringComparison.OrdinalIgnoreCase)); 
        }

        private bool ContainsWhiteSpace(string input)
        {
            return input.Any(char.IsWhiteSpace);
        }

        private UserInputModel GetUserInput(string userName)
        {
            Console.WriteLine();
            Console.Write("Enter FirstName: ");
            var firstName = Console.ReadLine();

            Console.WriteLine();
            Console.Write("Enter LastName: ");
            var lastName = Console.ReadLine();

            Console.WriteLine();
            Console.Write("Enter StreetAddress: ");
            var streetAddress = Console.ReadLine();

            Console.WriteLine();
            Console.Write("Enter City: ");
            var city = Console.ReadLine();

            Console.WriteLine();
            Console.Write("Enter State: ");
            var state = Console.ReadLine();

            Console.WriteLine();
            Console.Write("Enter PostalCode: ");
            var postalCode = Console.ReadLine();

            Console.WriteLine();
            Console.Write("Enter CellPhone: ");
            var cellPhone = Console.ReadLine();
            string formattedPhoneNumber = FormatPhoneNumberForDisplay(cellPhone);

            Console.WriteLine();
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
                UserId = Guid.NewGuid(),
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






