
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;

namespace Timesheets.DAL.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly TimesSheetsContext _db;

        public EmployeesRepository(TimesSheetsContext db) => _db = db;

        public async Task<ICollection<Employee>> GetAll()
        {
            var result = await System.Threading.Tasks.Task.Run<ICollection<Employee>>(() =>
            {
                return _db.Employees;
            });

            return result;
        }

        public async Task<Employee> GetById(int id)
        {
            var result = await System.Threading.Tasks.Task.Run<Employee>(() =>
            {
                return _db.Employees.SingleOrDefault(item => item.Id == id);
            });

            return result;
        }

        public async Task<ICollection<TaskExecution>> GetTaskExecutions(int employeeId)
        {
            var result = await System.Threading.Tasks.Task.Run<ICollection<TaskExecution>>(() =>
            {
                var employee = _db.Employees.SingleOrDefault(item => item.Id == employeeId);
                if (employee != null)
                    return employee.TaskExecutions;
                else 
                    return null;
            });

            return result;
        }


        public async Task<Employee> Create(Employee entity)
        {
            var result = await System.Threading.Tasks.Task.Run<Employee>(() =>
            {
                entity.Id = _db.Employees.Max(item => item.Id) + 1;
                _db.Employees.Add(entity);
                return entity;
            });

            return result;
        }
    }
}