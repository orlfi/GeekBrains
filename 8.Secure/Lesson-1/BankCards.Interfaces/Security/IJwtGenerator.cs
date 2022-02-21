using BankCards.Domain.Account;

namespace BankCards.Interfaces.Security;

public interface IJwtGenerator
{
    string CreateToken(AppUser user);
}
