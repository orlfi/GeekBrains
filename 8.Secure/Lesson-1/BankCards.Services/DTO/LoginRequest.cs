using BankCards.Interfaces.Data.Account;

namespace BankCards.Services.DTO;

public class LoginRequest : ILoginRequest
{
    public string UserName { get; init; }

    public string Password { get; init; }
}
