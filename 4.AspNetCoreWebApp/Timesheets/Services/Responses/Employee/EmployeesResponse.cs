using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Services.Responses.Employees
{
    public class EmployeesResponse
    {
        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
    }
}
