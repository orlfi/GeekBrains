using MailService.Domain.Entities.Base;
using ReportSender.Interfaces.Base;

namespace ReportSender.Domain.Entities;

public sealed class Employee : Entity, IEntity
{

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public IList<Order> Orders { get; set; } = new List<Order>();
}
