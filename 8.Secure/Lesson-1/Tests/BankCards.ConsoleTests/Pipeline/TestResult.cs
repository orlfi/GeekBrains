using BankCards.Domain;
using BankCards.Domain.Account;

namespace BankCards.ConsoleTests.Pipeline;

public class TestResult
{
    public IEnumerable<Card> CardsFromDb { get; set; } = null!;
    public IEnumerable<AppUser> UsersFromDb { get; set; } = null!;
    public IEnumerable<Card> CardsFromApi { get; set; } = null!;
}
