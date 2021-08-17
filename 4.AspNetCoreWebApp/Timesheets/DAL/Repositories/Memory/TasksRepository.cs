
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Task = Timesheets.DAL.Models.Task;

namespace Timesheets.DAL.Repositories.Memory
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TimesSheetsMemoryContext _db;

        public TasksRepository(TimesSheetsMemoryContext db) => _db = db;

        public async Task<ICollection<Task>> GetAll()
        {
            var result = await System.Threading.Tasks.Task.Run<ICollection<Task>>(() =>
            {
                return _db.Tasks;
            });

            return result;
        }

        public async Task<Task> GetById(int id)
        {
            var result = await System.Threading.Tasks.Task.Run<Task>(() =>
            {
                return _db.Tasks.SingleOrDefault(item => item.Id == id);
            });

            return result;
        }

        public async Task<Task> Create(Task entity)
        {
            var result = await System.Threading.Tasks.Task.Run<Task>(() =>
            {
                entity.Id = _db.Tasks.Max(item => item.Id) + 1;
                _db.Tasks.Add(entity);
                return entity;
            });

            return result;
        }
    }
}