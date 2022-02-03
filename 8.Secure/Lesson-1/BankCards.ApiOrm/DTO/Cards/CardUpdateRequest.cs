using System.ComponentModel.DataAnnotations;

namespace BankCards.ApiOrm.DTO.Cards;

public class CardUpdateRequest : CardCreateRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
}
