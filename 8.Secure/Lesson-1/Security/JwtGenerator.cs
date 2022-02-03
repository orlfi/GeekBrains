using BankCards.Domain.Account;
using BankCards.Interfaces.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankCards.Security;

public class JwtGenerator : IJwtGenerator
{
    private readonly SymmetricSecurityKey _key;

    public JwtGenerator(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }

    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.NameId, user.UserName) };

        var credidentals = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddSeconds(30),
            SigningCredentials = credidentals,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var result = tokenHandler.WriteToken(token);

        return result;
    }
}
