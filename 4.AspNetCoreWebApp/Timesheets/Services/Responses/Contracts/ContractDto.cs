using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Services.Responses.Contracts
{
    public class ContractDto
    {
        public int Id { get; set;}

        public string Number { get; set;}

        public string Name { get; set;}

        public decimal HourCost { get; set;}
    }
}
