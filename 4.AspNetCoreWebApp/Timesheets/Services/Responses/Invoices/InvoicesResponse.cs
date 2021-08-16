using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Services.Responses.Invoices
{
    public class InvoicesResponse
    {
        public List<InvoiceDto> Invoices { get; set; } = new List<InvoiceDto>();
    }
}
