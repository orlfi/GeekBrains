using System;
using System.Linq;
using System.Collections.Generic;

namespace Timesheets.DAL.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public int ContractId { get; set; }
        public Contract Contract { get; set; }

        public DateTime Date { get; set; }

        public decimal Total{ get; set; }

        public string Description { get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}