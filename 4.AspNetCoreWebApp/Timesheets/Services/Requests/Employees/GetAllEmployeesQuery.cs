using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Services.Responses.Employees;
using Timesheets.DAL.Interfaces;

using AutoMapper;

namespace  Timesheets.Services.Requests.Employees
{
    public class GetAllEmployeesQuery : IRequest<EmployeesResponse>
    {
        public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, EmployeesResponse>
        {
            private readonly IEmployeesRepository _repository;
            private readonly IMapper _mapper;

            public GetAllEmployeesQueryHandler(IEmployeesRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<EmployeesResponse> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
            {
                var Employees = await _repository.GetAll();

                var response = new EmployeesResponse();

                response.Employees.AddRange(_mapper.Map<List<EmployeeDto>>(Employees));

                return response;
            }
        }
    }
}
