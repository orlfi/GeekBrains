using BankCards.Domain.Account;
using BankCards.Interfaces.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;

namespace BankCards.Security;

public class JwtGenerator : IJwtGenerator
{
    private const int defaultTokenLifeTime = 300;

    private readonly SymmetricSecurityKey _key;
    private readonly int _tokenLifeTime;
    private readonly ILogger<JwtGenerator> _logger;

    public int TokenLifeTime => _tokenLifeTime == 0 ? defaultTokenLifeTime : _tokenLifeTime;

    public JwtGenerator(IConfiguration config, ILogger<JwtGenerator> logger)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        _tokenLifeTime = config.GetValue<int>("TokenLifeTime");
        _logger = logger;
    }

    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id)
        };

        var credidentals = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddSeconds(TokenLifeTime),
            SigningCredentials = credidentals,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var result = tokenHandler.WriteToken(token);

        _logger.LogDebug("Сформирован ключ {0} для пользователя {1} со временем жизни {2} до {3}",
                result,
                user,
                TokenLifeTime,
                tokenDescriptor.Expires?.ToString("dd.MM.yyyy hh:mm:ss")
            );

        return result;
    }
}
