using BankCards.DAL.Context;
using BankCards.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace BankCards.DAL.Configuration
{
    public class DataSeed
    {
        public static async Task SeedDataAsync(BankContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        FirstName = "Иван",
                        MiddleName = "Иванович",
                        LastName = "Иванов",
                        UserName = "TestUserFirst",
                        Email = "testuserfirst@test.com"
                    },

                    new AppUser
                    {
                        FirstName = "Иван",
                        MiddleName = "Иванович",
                        LastName = "Иванов",
                        UserName = "TestUserSecond",
                        Email = "testusersecond@test.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "qazWSX123!@#");
                }
            }
        }
    }
}