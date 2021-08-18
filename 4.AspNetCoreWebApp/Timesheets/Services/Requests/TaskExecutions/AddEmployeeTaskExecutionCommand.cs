using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Timesheets.DAL.Repositories;
using AutoMapper;
using Timesheets.Services.Responses.TaskExecutions;

namespace Timesheets.Services.Requests.TaskExecutions
{
    public class AddEmployeeTaskExecutionCommand : IRequest<TaskExecutionDto>
    {
        [FromRoute]
        public int TaskId { get; set; }

        [FromRoute]
        public int EmployeeId { get; set; }

        [FromRoute]
        public int TimeSpent { get; set; }

        public class AddEmployeeTaskExecutionCommandHandler : IRequestHandler<AddEmployeeTaskExecutionCommand, TaskExecutionDto>
        {
            private readonly ITaskExecutionsRepository _taskExecutionsRepository;
            private readonly IEmployeesRepository _employeesRepository;
            private readonly ITasksRepository _tasksRepository;
            private readonly IMapper _mapper;

            public AddEmployeeTaskExecutionCommandHandler(ITaskExecutionsRepository taskExecutionsRepository, IEmployeesRepository employeesRepository, ITasksRepository tasksRepository, IMapper mapper) => 
                (_taskExecutionsRepository, _employeesRepository, _tasksRepository, _mapper) = (taskExecutionsRepository, employeesRepository, tasksRepository, mapper);

            public async Task<TaskExecutionDto> Handle(AddEmployeeTaskExecutionCommand request, CancellationToken cancellationToken)
            {
                var task = await _tasksRepository.GetById(request.TaskId);
                var employee = await _employeesRepository.GetById(request.EmployeeId);

                if (employee != null && task != null)
                {
                    var taskExecution = _mapper.Map<TaskExecution>(request);
                    taskExecution = await _taskExecutionsRepository.Create(taskExecution);
                     await _taskExecutionsRepository.Create(taskExecution);
                     employee.TaskExecutions.Add(taskExecution);
                     task.TaskExecutions.Add(taskExecution);
                    _mapper.Map<TaskExecutionDto>(taskExecution);
                }

                return null;
            }
        }
    }
}
