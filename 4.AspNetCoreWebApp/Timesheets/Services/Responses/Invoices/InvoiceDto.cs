using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Services.Responses.Invoices
{
    public class InvoiceDto
    {
        public int Id { get; set;}

        public DateTime Date { get; set; }

        public decimal Total{ get; set; }

        public string Description { get; set; }
        
        // public ICollection<int> Tasks { get; set; } = new List<int>();    
    }
}
