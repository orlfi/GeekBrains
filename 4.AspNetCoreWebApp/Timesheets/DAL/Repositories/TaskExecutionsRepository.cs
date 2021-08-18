using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;

namespace Timesheets.DAL.Repositories
{
    public class TaskExecutionsRepository : ITaskExecutionsRepository
    {
        private readonly TimesSheetsContext _db;

        public TaskExecutionsRepository(TimesSheetsContext db) => _db = db;

        public async Task<ICollection<TaskExecution>> GetEmployeeTaskExecutions(Employee employee)
        {
            var result = await System.Threading.Tasks.Task.Run<ICollection<TaskExecution>>(() =>
            {
                return _db.TaskExecutions.Where(item => item.Employee == employee).ToList();
            });

            return result;
        }

        public async Task<TaskExecution> Create(TaskExecution entity)
        {
            var result = await System.Threading.Tasks.Task.Run<TaskExecution>(() =>
            {
                entity.Id = _db.TaskExecutions.Max(item => item.Id) + 1;
                _db.TaskExecutions.Add(entity);
                return entity;
            });

            return result;
        }
    }
}