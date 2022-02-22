using BankCards.Domain;
using BankCards.Domain.Account;

namespace BankCards.ConsoleTests;

internal static class Helpers
{
    public static void PrintCards(IEnumerable<Card> cards)
    {
        if (cards is  null)
            return;

        foreach (var item in cards)
        {
            Console.WriteLine($"{item.Id,3} {item.Number,16} {item.Owner,-20}");
        }
    }

    public static void PrintUsers(IEnumerable<AppUser> users)
    {
        if (users is null)
            return;

        foreach (var item in users)
        {
            Console.WriteLine($"{item.UserName,-20} {item.Email,-30} {item.LastName}.{item.FirstName.FirstOrDefault()}.{item.MiddleName.FirstOrDefault()} ");
        }
    }
}
