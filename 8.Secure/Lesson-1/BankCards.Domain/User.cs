using Microsoft.AspNetCore.Identity;

namespace BankCards.Domain;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
}
