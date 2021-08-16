using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Services.Responses.TaskExecutions
{
    public class TaskExecutionDto
    {
        public int Id { get; set;}

        public int TaskId { get; set; }

        public int EmployeeId { get; set; }

        public int TimeSpent { get; set; }
    }
}
