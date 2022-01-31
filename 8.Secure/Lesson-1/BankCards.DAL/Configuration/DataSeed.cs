using BankCards.DAL.Context;
using BankCards.Domain;
using BankCards.Domain.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankCards.DAL.Configuration
{
    public class DataSeed
    {
        public static async Task SeedDataAsync(BankContext context, UserManager<AppUser> userManager, CancellationToken cancel = default)
        {
            if (!await userManager.Users.AnyAsync(cancel).ConfigureAwait(false))
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
                    await userManager.CreateAsync(user, "qazWSX123!@#").ConfigureAwait(false);
                }
            }

            if (!await context.Cards.AnyAsync(cancel).ConfigureAwait(false))
            {
                var cards = new List<Card>()
                {
                    new Card
                    {
                        Id = 1,
                        Number = "1234567812345678",
                        Type = CardTypes.Visa,
                        Created = DateTime.Now,
                        ValidThru = DateTime.Now.AddYears(3),
                        Owner = "TEST OWNER 1",
                        Cvc = 111,
                    },
                    new Card
                    {
                        Id = 2,
                        Number = "1111222233334444",
                        Type = CardTypes.Visa,
                        Created = DateTime.Now,
                        ValidThru = DateTime.Now.AddYears(3),
                        Owner = "TEST OWNER 2",
                        Cvc = 222,
                    },
                    new Card
                    {
                        Id = 3,
                        Number = "1111111111111111",
                        Type = CardTypes.Visa,
                        Created = DateTime.Now,
                        ValidThru = DateTime.Now.AddYears(3),
                        Owner = "TEST OWNER 3",
                        Cvc = 333
                    }
                };
                await context.Cards.AddRangeAsync(cards, cancel).ConfigureAwait(false);
                await context.SaveChangesAsync(cancel).ConfigureAwait(false);
            }
        }
    }
}