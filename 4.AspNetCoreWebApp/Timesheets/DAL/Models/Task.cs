using System;
using System.Collections.Generic;

namespace Timesheets.DAL.Models
{
    public class Task
    {
        public int Id { get; set;}
        
        public string Name { get; set;}

        public int Amount { get; set;}

        public bool IsCompleted { get; set;}

        // public int ContractId { get; set;}

        // public Contract Contract { get; set;}

        public ICollection<TaskExecution> TaskExecutions { get; set;}  = new List<TaskExecution>();
        //public ICollection<Invoice> Invoices { get; set;}
    }
}