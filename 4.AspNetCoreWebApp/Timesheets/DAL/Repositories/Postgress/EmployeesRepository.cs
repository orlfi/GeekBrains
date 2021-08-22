using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Timesheets.DAL.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly TimesSheetsContext _db;

        public EmployeesRepository(TimesSheetsContext db) => _db = db;

        public async Task<ICollection<Employee>> GetAll()
        {
            return await _db.Employees.ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
                return await _db.Employees.SingleOrDefaultAsync(item => item.Id == id);
        }

        // public async Task<ICollection<TaskExecution>> GetTaskExecutions(int employeeId)
        // {
        //     var result = await System.Threading.Tasks.Task.Run<ICollection<TaskExecution>>(() =>
        //     {
        //         var employee = _db.Employees.SingleOrDefault(item => item.Id == employeeId);
        //         if (employee != null)
        //             return employee.TaskExecutions;
        //         else 
        //             return null;
        //     });

        //     return result;
        // }


        public async Task<Employee> Create(Employee entity)
        {
            try
            {
                await _db.Employees.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception($"Ошибка при создании нового работника: {ex.Message}");
            }
        }
    }
}