using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.DAL.Models;

namespace Timesheets.DAL.Interfaces
{
    public interface ITaskExecutionsRepository
    {
        Task<ICollection<TaskExecution>> GetEmployeeTaskExecutions(int employeeId);

        Task<TaskExecution> Create(TaskExecution entity);
    }
}