using BankCards.Interfaces.Data.Account;

namespace BankCards.Services.DTO;

public class RegisterUserRequest : IRegisterUserRequest
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}
