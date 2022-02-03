//using System.Globalization;
//using BankCards.ConsoleTests.DTO.Cards;
//using BankCards.Domain;

//namespace BankCards.ConsoleTests.Mappings;

//public static class CardMappings
//{
//    public static Card? ToCard(this CardResponse? response) =>
//        response is null
//        ? null
//        : new Card()
//        {
//            Id = response.Id,
//            Number = response.Number,
//            Type = (CardTypes)response.Type,
//            Created = response.Created,
//            ValidThru = response.ValidThru,
//            Owner = response.Owner
//        };

//    public static IEnumerable<Card> ToCard(this IEnumerable<CardResponse> response) =>
//        response.Select(item => item.ToCard()!);
//}
