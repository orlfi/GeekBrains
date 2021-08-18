using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Services.Responses.Tasks
{
    public class TasksResponse
    {
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
