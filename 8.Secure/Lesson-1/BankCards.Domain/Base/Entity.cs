using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankCards.Domain.Base
{
    public abstract class Entity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}