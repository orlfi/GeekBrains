using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MailService.Domain.Entities.Base;

namespace MailService.Domain.Entities;

public sealed class Employee : Entity
{

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public IList<Order> Orders { get; set; } = new List<Order>();
}
