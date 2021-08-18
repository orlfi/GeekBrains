using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Timesheets.DAL.Interfaces
{
    public interface IContractsRepository
    {
        Task<ICollection<Contract>> GetByCustomer(Customer customer);
        
        Task<Contract> GetById(int id);
        
        Task<Contract> Create(Contract entity);
    }
}