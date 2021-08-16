using System;
using System.Collections.Generic;

namespace Timesheets.DAL.Models
{
    public class Contract
    {
        public int Id { get; set;}

        // public int ClientId { get; set;}
        
        public string Number { get; set;}

        // public DateTime Date { get; set;}

        public string Name { get; set;}

        public decimal HourCost { get; set;}
        
        public Customer Customer { get; set;}

        public ICollection<Invoice> Invoices { get; set;} = new List<Invoice>();
        
        // public ICollection<Task> Tasks { get; set;}
    }
}