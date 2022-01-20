using BankCards.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankCards.DAL.Configuration;

public class CardsConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Number).IsRequired().HasMaxLength(16);
        builder.Property(p => p.Type).IsRequired();
        builder.Property(p => p.Owner).IsRequired().HasMaxLength(100); ;
        builder.Property(p => p.Created).IsRequired();
        builder.Property(p => p.ValidThru).IsRequired();
        builder.Property(p => p.Created).IsRequired();
        builder.Property(p => p.ValidThru).IsRequired();

        builder.HasData
        (
            new Card
            {
                Id = 1,
                Number = "1234567812345678",
                Type = CardTypes.Visa,
                Created = DateTime.Now,
                ValidThru = DateTime.Now.AddYears(3),
                Owner = "TEST OWNER 1",
                Cvc = 111,
            },
            new Card
            {
                Id = 2,
                Number = "1111222233334444",
                Type = CardTypes.Visa,
                Created = DateTime.Now,
                ValidThru = DateTime.Now.AddYears(3),
                Owner = "TEST OWNER 2",
                Cvc = 222,
            },
            new Card
            {
                Id = 3,
                Number = "1111111111111111",
                Type = CardTypes.Visa,
                Created = DateTime.Now,
                ValidThru = DateTime.Now.AddYears(3),
                Owner = "TEST OWNER 3",
                Cvc = 333
            }
        );
    }
}
