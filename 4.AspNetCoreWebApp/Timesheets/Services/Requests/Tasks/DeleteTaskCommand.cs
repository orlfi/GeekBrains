using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using AutoMapper;
using Timesheets.Services.Responses.Tasks;

namespace Timesheets.Services.Requests.Tasks
{
    public class DeleteTaskCommand : IRequest
    {
        public int Id { get; set;}

        public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
        {
            private readonly ITasksRepository _repository;
            private readonly IMapper _mapper;

            public  DeleteTaskCommandHandler(ITasksRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async System.Threading.Tasks.Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
            {
                await _repository.Delete(request.Id);
                return Unit.Value;
            }
        }
    }
}
