using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using AutoMapper;
using Timesheets.Services.Responses.Employees;

namespace Timesheets.Services.Requests.Employees
{
    public class DeleteEmployeeCommand : IRequest
    {
        public int Id { get; set;}

        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
        {
            private readonly IEmployeesRepository _repository;
            private readonly IMapper _mapper;

            public  DeleteEmployeeCommandHandler(IEmployeesRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async System.Threading.Tasks.Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                await _repository.Delete(request.Id);
                return Unit.Value;
            }
        }
    }
}
