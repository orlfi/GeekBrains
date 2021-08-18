using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Services.Responses.Contracts
{
    public class ContractsResponse
    {
        public List<ContractDto> Contracts { get; set; } = new List<ContractDto>();
    }
}
