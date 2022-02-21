using MailService.Domain.Entities.Base;
using ReportSender.Interfaces.Base;

namespace ReportSender.Domain.Entities;

public sealed class Product : Entity, IEntity
{
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
}
