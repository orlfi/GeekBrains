using System.ComponentModel.DataAnnotations;

namespace BankCards.ApiOrm.DTO.Cards;

public class BookUpdateRequest : BookCreateRequest
{
    public string Id { get; set; }
}
