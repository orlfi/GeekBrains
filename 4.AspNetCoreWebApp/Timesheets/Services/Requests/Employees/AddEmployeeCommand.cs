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
    public class AddEmployeeCommand : IRequest<EmployeeDto>
    {
        public string Name { get; set;}

        public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, EmployeeDto>
        {
            private readonly IEmployeesRepository _repository;
            private readonly IMapper _mapper;

            public  AddEmployeeCommandHandler(IEmployeesRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async Task<EmployeeDto> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
            {
                var Employee = await _repository.Create(_mapper.Map<Employee>(request));
                return _mapper.Map<EmployeeDto>(Employee);
            }
        }
    }
}
