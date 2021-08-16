using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.DAL.Models;

namespace Timesheets.DAL.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<ICollection<Employee>> GetAll();

        Task<Employee> GetById(int id);

        Task<ICollection<TaskExecution>> GetTaskExecutions(int employeeId);


        Task<Employee> Create(Employee entity);

        Task<Employee> Update(Employee entity);

        System.Threading.Tasks.Task Delete(int id);
    }
}