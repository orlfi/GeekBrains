using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailService.Interfaces.Common;

namespace MailService.Domain.Entities.Base
{

    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}