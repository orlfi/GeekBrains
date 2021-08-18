using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.DAL.Models;
using Task = Timesheets.DAL.Models.Task;

namespace Timesheets.DAL.Interfaces
{
    public interface ITasksRepository
    {
        Task<ICollection<Task>> GetAll();
        
        Task<Task> GetById(int id);
        
        Task<Task> Create(Task entity);
    }
}