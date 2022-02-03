using System.ComponentModel.DataAnnotations;

namespace BankCards.ConsoleTests.DTO.Cards;

public class CardUpdateRequest : CardCreateRequest
{
    /// <summary>
    /// Id
    /// </summary>
    [Required(ErrorMessage = "Укажите ID карты")]
    public int Id { get; set; }
}
