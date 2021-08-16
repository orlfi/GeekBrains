using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Services.Responses.Customers
{
    public class CustomersResponse
    {
        public List<CustomerDto> Customers { get; set; } = new List<CustomerDto>();
    }
}
