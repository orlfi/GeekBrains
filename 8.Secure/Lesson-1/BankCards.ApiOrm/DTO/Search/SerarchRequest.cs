namespace BankCards.ApiOrm.DTO.Search;

public class SerarchRequest
{
    /// <summary>
    /// Номер карты
    /// </summary>
    /// <value>16 цифр</value>
    public string Text { get; set; } = string.Empty;
}
