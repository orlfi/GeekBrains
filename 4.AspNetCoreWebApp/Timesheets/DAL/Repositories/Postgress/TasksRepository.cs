
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Task = Timesheets.DAL.Models.Task;
using Microsoft.EntityFrameworkCore;

namespace Timesheets.DAL.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TimesSheetsContext _db;

        public TasksRepository(TimesSheetsContext db) => _db = db;

        public async Task<ICollection<Task>> GetAll()
        {
            return await _db.Tasks.ToListAsync();
        }

        public async Task<Task> GetById(int id)
        {
            return await _db.Tasks.SingleOrDefaultAsync(item => item.Id == id);
        }

        public async Task<Task> Create(Task entity)
        {
            try
            {
                await _db.Tasks.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception($"Ошибка при создании нового задания: {ex.Message}");
            }
        }

        public async Task<ICollection<Task>> GetByIdList(IList<int> idList)
        {
            return await _db.Tasks.Where(item => idList.Contains(item.Id)).Include(p => p.Invoice).ToListAsync();
        }
    }
}