using BankCards.DAL.Context;
using BankCards.Services.DTO.Users.Authentication;
using Microsoft.Extensions.Logging;

namespace BankCards.Services;

public class UserManager
{
    private readonly BankContext _db;
    private readonly ILogger<UserManager> _logger;

    public UserManager(BankContext db, ILogger<UserManager> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task Login(LoginRequest login)
    {
    }
}
