using Microsoft.AspNetCore.Identity;

namespace BankCards.Domain.Account;

public class AppUser : IdentityUser
{
    /// <summary>
    /// ���
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// ��������
    /// </summary>
    public string MiddleName { get; set; } = string.Empty;

    /// <summary>
    /// �������
    /// </summary>
    public string LastName { get; set; } = string.Empty;
}
