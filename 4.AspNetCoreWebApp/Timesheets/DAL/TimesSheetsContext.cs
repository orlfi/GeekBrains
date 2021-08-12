using System.Collections.Generic;
using  Timesheets.DAL.Models;

namespace Timesheets.DAL
{
    public class TimesSheetsContext
    {
        ICollection<Contract> Contracts {get; set;}   
        ICollection<Customer> Customers {get; set;}   
        ICollection<Employee> Employees {get; set;}   
        ICollection<Task> Tasks {get; set;}   
        ICollection<Invoice> Invoices {get; set;}   
        ICollection<TaskExecution> TaskExecutions {get; set;}   
    }
}