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

        Task<Task> Update(Task entity);

        System.Threading.Tasks.Task Delete(int id);

        // IList<AgentInfo> GetRegistered();

        // void EnableById(int agentId);

        // void DisableById(int agentId);
    }
}