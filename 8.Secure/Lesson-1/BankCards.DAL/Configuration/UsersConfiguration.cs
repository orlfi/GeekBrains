using BankCards.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankCards.DAL.Configuration;

public class UsersConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasData
        (
            new AppUser
            {
                DisplayName = "TestUserFirst",
                UserName = "TestUserFirst",
                Email = "testuserfirst@test.com"
            },
            new AppUser
            {
                DisplayName = "TestUserSecond",
                UserName = "TestUserSecond",
                Email = "testusersecond@test.com"
            }
        );
    }
}
