using MailService.Domain.Entities.Base;
using ReportSender.Interfaces.Base;

namespace ReportSender.Domain.Entities;

public sealed class Order : Entity, IEntity
{
    public Product Product { get; set; } = null!;

    public int Count { get; set; }

    public decimal Total => Count * Product.Price;
}
