using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Services.Responses.TaskExecutions
{
    public class TaskExecutionsResponse
    {
        public List<TaskExecutionDto> TaskExecutions { get; set; } = new List<TaskExecutionDto>();
    }
}
