using System.Collections.Generic;

namespace Timesheets.DAL.Models
{
    public class Employee
    {
        public int Id { get; set;}
        
        public string Name { get; set;}

        public ICollection<TaskExecution> TaskExecutions { get; set;}
    }
}