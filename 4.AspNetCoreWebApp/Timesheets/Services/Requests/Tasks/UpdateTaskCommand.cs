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
    public class UpdateTaskCommand : IRequest<TaskDto>
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public int Amount { get; set;}

        public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
        {
            private readonly ITasksRepository _repository;
            private readonly IMapper _mapper;

            public  UpdateTaskCommandHandler(ITasksRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
            {
                var Task = await _repository.Update(_mapper.Map<Task>(request));
                return _mapper.Map<TaskDto>(Task);
            }
        }
    }
}
