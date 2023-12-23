using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace HorseAuction
{
    public class AuthenticationService
    {
        private readonly AuctionDbContext dbContext;
        private readonly ILogger<AuthenticationService> logger;

        public AuthenticationService(AuctionDbContext dbContext, ILogger<AuthenticationService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public User AuthenticateUser()
        {
            User authenticatedUser = null;

            do
            {
                Console.Write("Enter your UserName: ");
                string username = Console.ReadLine();

                authenticatedUser = GetUserByUsername(username);

                if (authenticatedUser != null)
                {
                    LogWarningAndConsole("User not found. Please enter a valid username.");
                }

            } while (authenticatedUser == null);
            return authenticatedUser;
        }

        private User GetUserByUsername(string username)
        {
            try
            {
                return dbContext.Users.SingleOrDefault(u => u.UserName.ToLower() == username.ToLower());
            }
            catch (Exception ex)
            {
                LogErrorAndConsole($"Error while retrieving user by username: {ex.Message}");
                return null;
            }
        }
        private void LogErrorAndConsole(string errorMessage, [CallerMemberName] string methodName = "")
        {
            Console.WriteLine(errorMessage);
            logger.LogError($"Error in {methodName}: {errorMessage}");
        }
        private void LogWarningAndConsole(string warningMessage, [CallerMemberName] string methodName = "")
        {
            Console.WriteLine(warningMessage);
            logger.LogWarning($"Warning in {methodName}: {warningMessage}");
        }
    }
}