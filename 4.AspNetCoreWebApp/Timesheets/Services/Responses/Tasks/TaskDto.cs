using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Services.Responses.Tasks
{
    public class TaskDto
    {
        public int Id { get; set;}
        
        public string Name { get; set;}
        
        public int Amount { get; set;}
    
        public bool IsCompleted { get; set;}
    }
}
