using BankCards.DAL.Context;
using BankCards.Domain.Account;
using BankCards.Domain.Core;
using BankCards.Interfaces;
using BankCards.Interfaces.Data;
using BankCards.Interfaces.Data.Account;
using BankCards.Interfaces.Security;
using BankCards.Services.DTO;
using BankCards.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BankCards.Services;

public class AccountManager : IAccountManager
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly BankContext _db;
    private readonly ILogger<AccountManager> _logger;

    public AccountManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator, BankContext db, ILogger<AccountManager> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _db = db;
        _logger = logger;
    }

    public async Task<IResult<ILoginResponse>> Login(ILoginRequest login, CancellationToken cancel = default)
    {
        Result<ILoginResponse> result;

        var user = await _userManager.FindByNameAsync(login.UserName);
        if (user == null)
        {
            result = new ErrorInformation($"The user with the name  {login.UserName} was not found");
            return result;
        }

        var loginResult = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (loginResult.Succeeded)
        {
            _logger.LogInformation("The user {0} has been successfully logged in", login.UserName);
            result = new LoginResponse()
            {
                Token = _jwtGenerator.CreateToken(user)
            };
            return result;
        }

        _logger.LogInformation("User {0} authentication error", login.UserName);
        result = new ErrorInformation("Authentication error");
        return result;
    }

    public async Task<IResult<IRegisterUserResponse>> Register(IRegisterUserRequest registerUser, CancellationToken cancel = default)
    {
        Result<IRegisterUserResponse> result;

        if (registerUser is null)
            throw new ArgumentNullException(nameof(registerUser));

        var user = new AppUser()
        {
            UserName = registerUser.UserName,
            Email = registerUser.Email
        };

        var registerResult = await _userManager.CreateAsync(user, registerUser.Password).ConfigureAwait(false);

        if (registerResult.Succeeded)
        {
            result = new RegisterUserResponse()
            {
                Token = _jwtGenerator.CreateToken(user)
            };
            return result;
        }

        result = registerResult.Errors.Select(item => new ErrorInformation(item.Description)).ToArray();
        return result;
    }
}
