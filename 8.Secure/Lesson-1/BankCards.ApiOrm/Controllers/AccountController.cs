using System.Reflection;
using System.Security.Claims;
using BankCards.Interfaces;
using BankCards.Interfaces.Data.Account;
using BankCards.Services.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;

namespace BankCards.ApiOrm.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly IAccountManager _accountManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountManager accountManager, ILogger<AccountController> logger)
    {
        _accountManager = accountManager;
        _logger = logger;
    }

    [HttpGet("GetClaims")]
    public IActionResult GetClaims()
    {
        var indentity = User.Identity as ClaimsIdentity;
        if (indentity is not null)
        {
            var result = _accountManager.GetClaimsInfo(indentity.Claims);
            return Ok(result);
        }
        else
        {
            return BadRequest("Indentity is null");
        }
    }

    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    /// <param name="loginRequest">Логин и пароль</param>
    /// <returns>Bearer token</returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var loginResult = await _accountManager.Login(loginRequest);

        if (loginResult.Succeeded)
            return Ok(loginResult.Data);

        return BadRequest(loginResult.Errors);
    }

    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    /// <param name="registerUserRequest">Логин, email и пароль</param>
    /// <returns>Bearer token</returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterUserRequest registerUserRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var registerResult = await _accountManager.Register(registerUserRequest);

        if (registerResult.Succeeded)
            return Ok(registerResult.Data);

        return BadRequest(registerResult.Errors);
    }
}
