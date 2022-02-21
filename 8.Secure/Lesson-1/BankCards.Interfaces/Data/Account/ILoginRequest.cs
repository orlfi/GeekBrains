namespace BankCards.Interfaces.Data.Account;

public interface ILoginRequest
{
    string Password { get; init; }
    string UserName { get; init; }
}
