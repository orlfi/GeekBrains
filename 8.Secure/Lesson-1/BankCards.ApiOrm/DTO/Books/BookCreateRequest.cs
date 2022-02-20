namespace BankCards.ApiOrm.DTO.Cards;

public class BookCreateRequest
{
    public string BookName { get; set; }

    public decimal Price { get; set; }

    public string Category { get; set; }

    public string Author { get; set; }
}
