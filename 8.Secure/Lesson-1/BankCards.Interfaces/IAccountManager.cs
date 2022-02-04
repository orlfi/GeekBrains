using System.Security.Claims;
using BankCards.Interfaces.Data;
using BankCards.Interfaces.Data.Account;

namespace BankCards.Interfaces;

public interface IAccountManager
{
    IEnumerable<object> GetClaimsInfo(IEnumerable<Claim> claims);
    Task<IResult<ILoginResponse>> Login(ILoginRequest login, CancellationToken cancel = default);
    Task<IResult<IRegisterUserResponse>> Register(IRegisterUserRequest registerUser, CancellationToken cancel = default);
}
