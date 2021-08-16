using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Services.Responses.Tasks;
using Timesheets.DAL.Interfaces;

using AutoMapper;

namespace  Timesheets.Services.Requests.Tasks
{
    public class GetAllTasksQuery : IRequest<TasksResponse>
    {
        public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, TasksResponse>
        {
            private readonly ITasksRepository _repository;
            private readonly IMapper _mapper;

            public GetAllTasksQueryHandler(ITasksRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<TasksResponse> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
            {
                var Tasks = await _repository.GetAll();

                var response = new TasksResponse();

                response.Tasks.AddRange(_mapper.Map<List<TaskDto>>(Tasks));

                return response;
            }
        }
    }
}
