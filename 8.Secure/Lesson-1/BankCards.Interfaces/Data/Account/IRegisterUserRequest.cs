namespace BankCards.Interfaces.Data.Account;

public interface IRegisterUserRequest
{
    string Email { get; init; }

    string Password { get; init; }

    string UserName { get; init; }
}
