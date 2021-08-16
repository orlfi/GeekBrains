using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Timesheets.Services.Responses.TaskExecutions;
using Timesheets.DAL.Interfaces;

using AutoMapper;

namespace Timesheets.Services.Requests.Employees
{
    public class GetEmployeeExecutionsQuery : IRequest<TaskExecutionsResponse>
    {
        [FromRoute]
        public int EmployeeId { get; set; }

        public class GetEmployeeExecutionsQueryHandler : IRequestHandler<GetEmployeeExecutionsQuery, TaskExecutionsResponse>
        {

            private readonly IEmployeesRepository _employeesRepository;
            private readonly IMapper _mapper;

            public GetEmployeeExecutionsQueryHandler(IEmployeesRepository employeesRepository, IContractsRepository contractsRepository, IMapper mapper) =>
                (_employeesRepository,_mapper) = (employeesRepository, mapper);

            public async Task<TaskExecutionsResponse> Handle(GetEmployeeExecutionsQuery request, CancellationToken cancellationToken)
            {
                    var executions = await _employeesRepository.GetTaskExecutions(request.EmployeeId);

                    var response = new TaskExecutionsResponse();
                    response.TaskExecutions.AddRange(_mapper.Map<List<TaskExecutionDto>>(executions));

                    return response;
            }
        }
    }
}
