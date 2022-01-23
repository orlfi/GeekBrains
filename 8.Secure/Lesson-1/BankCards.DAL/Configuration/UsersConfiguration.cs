//using BankCards.Domain.Account;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace BankCards.DAL.Configuration;

//public class UsersConfiguration : IEntityTypeConfiguration<AppUser>
//{
//    public void Configure(EntityTypeBuilder<AppUser> builder)
//    {
//        builder.HasData
//        (
//            new AppUser
//            {
//                FirstName = "Иван",
//                MiddleName = "Иванович",
//                LastName = "Иванов",
//                UserName = "First",
//                Email = "testuserfirst@test.com"
//            },
//            new AppUser
//            {
//                FirstName = "Петр",
//                MiddleName = "Петрович",
//                LastName = "Петров",
//                UserName = "Second",
//                Email = "testusersecond@test.com"
//            }
//        );
//    }
//}
