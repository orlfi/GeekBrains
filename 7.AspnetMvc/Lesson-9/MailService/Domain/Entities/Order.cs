using MailService.Domain.Entities.Base;

namespace MailService.Domain.Entities;

public sealed class Order : Entity
{
    public Product Product { get; set; } = null!;

    public int Count { get; set; }

    public decimal Total => Count * Product.Price;
}
