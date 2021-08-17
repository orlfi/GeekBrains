using System;
using System.Collections.Generic;

namespace Timesheets.DAL.Models
{
    public class Contract
    {
        public int Id { get; set;}

        public string Number { get; set;}

        public string Name { get; set;}

        public decimal HourCost { get; set;}
        
        public int  CustomerId { get; set;}
        
        public Customer Customer { get; set;}

        public ICollection<Invoice> Invoices { get; set;}
        
        // public ICollection<Task> Tasks { get; set;}
    }
}