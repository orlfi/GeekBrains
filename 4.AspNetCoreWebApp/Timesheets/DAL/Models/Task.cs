using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timesheets.DAL.Models
{
    public class Task
    {
        public int Id { get; set;}
        
        [Required]
        public string Name { get; set;}

        public int Amount { get; set;}

        public bool IsCompleted { get; set;}

        public ICollection<TaskExecution> TaskExecutions { get; set;}
        
        public int? InvoiceId { get; set;}
        
        public Invoice Invoice { get; set;}
    }
}