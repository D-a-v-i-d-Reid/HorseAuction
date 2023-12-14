using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HorseAuction
{
    public class AuthenticationService
    {
        private readonly AuctionDbContext dbContext;

        public AuthenticationService(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                    Console.WriteLine("User not found. Please enter a valid username.");
                }

            } while (authenticatedUser == null);
            return authenticatedUser;
        }

        private User GetUserByUsername(string username)
        {
            using (var context = new AuctionDbContext())
            {
                return context.Users.SingleOrDefault(u => u.UserName.ToLower() == username.ToLower());
            }
        }
    }

}
