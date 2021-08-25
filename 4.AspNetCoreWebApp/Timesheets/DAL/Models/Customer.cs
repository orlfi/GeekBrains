using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timesheets.DAL.Models
{
    public class Customer
    {
        public int Id { get; set;}
        
        [Required]
        public string Name { get; set;}
        
        public  ICollection<Contract> Contracts { get; set;}
    }
}