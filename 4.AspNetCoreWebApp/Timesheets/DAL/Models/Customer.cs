using System.Collections.Generic;

namespace Timesheets.DAL.Models
{
    public class Customer
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public ICollection<Contract> Contracts { get; set;}
    }
}