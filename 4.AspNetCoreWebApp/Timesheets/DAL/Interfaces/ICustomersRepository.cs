using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Timesheets.DAL.Interfaces
{
    public interface ICustomersRepository
    {
        Task<ICollection<Customer>> GetAll();

        Task<Customer> GetById(int id);

        Task<Customer> Create(Customer entity);
    }
}