using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailService.Domain.Entities.Base;

namespace MailService.Domain.Entities
{
    public sealed class Product: Entity
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
    }
}