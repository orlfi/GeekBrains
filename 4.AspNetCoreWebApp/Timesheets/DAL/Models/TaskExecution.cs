using System;
namespace Timesheets.DAL.Models
{
    public class TaskExecution
    {
        public int Id { get; set;}
        public string TaskId { get; set;}
        public string EmployeeId { get; set;}
        public int TimeSpent { get; set;}
    }
}