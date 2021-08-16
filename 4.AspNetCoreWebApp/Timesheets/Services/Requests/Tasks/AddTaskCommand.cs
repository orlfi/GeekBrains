using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using AutoMapper;
using Timesheets.Services.Responses.Tasks;
using Task = Timesheets.DAL.Models.Task;

namespace Timesheets.Services.Requests.Tasks
{
    public class AddTaskCommand : IRequest<TaskDto>
    {
        public string Name { get; set;}
        public int Amount { get; set;}

        public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, TaskDto>
        {
            private readonly ITasksRepository _repository;
            private readonly IMapper _mapper;

            public  AddTaskCommandHandler(ITasksRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async Task<TaskDto> Handle(AddTaskCommand request, CancellationToken cancellationToken)
            {
                var Task = await _repository.Create(_mapper.Map<Task>(request));
                return _mapper.Map<TaskDto>(Task);
            }
        }
    }
}
