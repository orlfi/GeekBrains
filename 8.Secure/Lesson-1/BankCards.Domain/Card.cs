using BankCards.Domain.Base;

namespace BankCards.Domain;

public class Card : Entity
{
    /// <summary>
    /// Номер карты
    /// </summary>
    /// <value>16 цифр</value>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Тип карты
    /// </summary>
    public CardTypes Type { get; set; }

    /// <summary>
    /// Дата изготовления
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Срок действия по
    /// </summary>
    public DateTime ValidThru { get; set; }

    /// <summary>
    /// Владелец
    /// </summary>
    /// <value>Только латинские буквы</value>
    public string Owner { get; set; } = string.Empty;

    /// <summary>
    /// Защитный код
    /// </summary>
    /// <value>3 цифры</value>
    public int Cvc { get; set; }
}