using System.ComponentModel.DataAnnotations;
using BankCards.ApiSql.DTO.Cards;

namespace BankCards.ApiSql.DTO.Cards;

public class CardUpdateRequest : CardCreateRequest
{
    /// <summary>
    /// Id
    /// </summary>
    [Required(ErrorMessage = "Укажите ID карты")]
    public int Id { get; set; }
}
