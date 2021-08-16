
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Task = Timesheets.DAL.Models.Task;

namespace Timesheets.DAL.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TimesSheetsContext _db;

        public TasksRepository(TimesSheetsContext db) => _db = db;

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

        public async Task<Task> Update(Task entity)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                var updatedTask = _db.Tasks.SingleOrDefault(item => item.Id == entity.Id);
                if (updatedTask != null)
                {
                    updatedTask.Name = entity.Name;
                    var index = _db.Tasks.IndexOf(updatedTask);
                    _db.Tasks[index] = entity;
                }
            });

            return entity;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            await System.Threading.Tasks.Task.Run(() =>
           {
               var Task = _db.Tasks.FirstOrDefault(item => item.Id == id);
               if (Task != null)
               {
                   _db.Tasks.Remove(Task);
               }
           });
        }
    }
}