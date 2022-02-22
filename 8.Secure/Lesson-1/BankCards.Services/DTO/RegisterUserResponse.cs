using BankCards.Interfaces.Data.Account;

namespace BankCards.Services.DTO;

public class RegisterUserResponse : IRegisterUserResponse
{
    public string Token { get; init; }
}
