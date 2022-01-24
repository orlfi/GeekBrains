using System.ComponentModel.DataAnnotations;

namespace BankCards.ApiOrm.DTO.Cards;

public class CardCreateRequest
{
    /// <summary>
    /// Номер карты
    /// </summary>
    /// <value>16 цифр</value>
    [Required(ErrorMessage = "Укажите номер карты")]
    [StringLength(16, MinimumLength =16, ErrorMessage = "Длина должна быть 16 символов")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Тип карты
    /// </summary>
    [Required(ErrorMessage = "Укажите тип карты")]
    public int Type { get; set; }

    /// <summary>
    /// Дата изготовления
    /// </summary>
    [Required(ErrorMessage = "Укажите дату создания карты")]
    public DateTime Created { get; set; }

    /// <summary>
    /// Срок действия по
    /// </summary>
    [Required(ErrorMessage = "Укажите срок действия карты")]
    public DateTime ValidThru { get; set; }

    /// <summary>
    /// Владелец
    /// </summary>
    /// <value>Только латинские буквы</value>
    [Required(ErrorMessage = "Укажите владельца карты")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 100 символов")]
    public string Owner { get; set; } = string.Empty;

    /// <summary>
    /// Защитный код
    /// </summary>
    /// <value>3 цифры</value>
    [Required(ErrorMessage = "Укажите код CVC")]
    [Range(100, 999, ErrorMessage = "Код должен быть 3х значным")]
    public int Cvc { get; set; }
}
