using BankCards.Interfaces.Data.Account;

namespace BankCards.Services.DTO;

public class LoginResponse : ILoginResponse
{
    public string Token { get; init; }
}
