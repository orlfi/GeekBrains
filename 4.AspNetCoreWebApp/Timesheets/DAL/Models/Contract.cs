using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timesheets.DAL.Models
{
    public class Contract
    {
        public int Id { get; set;}

        [Required]
        public string Number { get; set;}

        [Required]
        public string Name { get; set;}

        public decimal HourCost { get; set;}
        
        public int  CustomerId { get; set;}
        
        public  Customer Customer { get; set;}

        public  ICollection<Invoice> Invoices { get; set;}
    }
}