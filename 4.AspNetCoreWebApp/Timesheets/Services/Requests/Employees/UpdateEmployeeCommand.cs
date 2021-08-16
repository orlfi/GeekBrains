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
    public class UpdateEmployeeCommand : IRequest<EmployeeDto>
    {
        public int Id { get; set;}
        public string Name { get; set;}

        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
        {
            private readonly IEmployeesRepository _repository;
            private readonly IMapper _mapper;

            public  UpdateEmployeeCommandHandler(IEmployeesRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var Employee = await _repository.Update(_mapper.Map<Employee>(request));
                return _mapper.Map<EmployeeDto>(Employee);
            }
        }
    }
}
