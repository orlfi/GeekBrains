using BankCards.ApiOrm.DTO.Cards;
using BankCards.Domain;

namespace BankCards.ApiOrm.Mappers;

public static class CardMappers
{
    public static Card ToCard(this CardCreateRequest request) =>
        new Card()
        {
            Number = request.Number,
            Type = (CardTypes)request.Type,
            Created = request.Created,
            ValidThru = request.ValidThru,
            Cvc = request.Cvc,
            Owner = request.Owner,
        };

    public static Card ToCard(this CardUpdateRequest request) =>
        new Card()
        {
            Id = request.Id,
            Number = request.Number,
            Type = (CardTypes)request.Type,
            Created = request.Created,
            ValidThru = request.ValidThru,
            Cvc = request.Cvc,
            Owner = request.Owner,
        };

    public static CardResponse? ToResponse(this Card? entity) =>
        entity is null ? null :
        new CardResponse()
        {
            Id = entity.Id,
            Number = entity.Number,
            Type = (int)entity.Type,
            Created = entity.Created,
            ValidThru = entity.ValidThru,
            Cvc = entity.Cvc,
            Owner = entity.Owner,
        };

    public static IEnumerable<CardResponse> ToResponse(this IEnumerable<Card> entity) =>
        entity.Select(x => x.ToResponse()!);
    // {
    //     Cards = entity.Select(x => x.ToResponse()!),
    // };
}
