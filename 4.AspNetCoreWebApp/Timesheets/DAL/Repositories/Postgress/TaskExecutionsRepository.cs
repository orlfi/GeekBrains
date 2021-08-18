using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Task = System.Threading.Tasks.Task;
using Microsoft.EntityFrameworkCore;


namespace Timesheets.DAL.Repositories
{
    public class TaskExecutionsRepository : ITaskExecutionsRepository
    {
        private readonly TimesSheetsContext _db;

        public TaskExecutionsRepository(TimesSheetsContext db) => _db = db;

        public async Task<ICollection<TaskExecution>> GetEmployeeTaskExecutions(int employeeId)
        {
            return await _db.TaskExecutions.Where(item => item.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<TaskExecution> Create(TaskExecution entity)
        {
            try
            {
                await _db.TaskExecutions.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception($"Ошибка добавления исполнения задания Id={entity.TaskId} пользователем: {entity.EmployeeId} : {ex.Message}");
            }

        }
    }
}