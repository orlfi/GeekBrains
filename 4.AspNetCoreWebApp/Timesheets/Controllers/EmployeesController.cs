using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using Timesheets.Services.Requests.Employees;
using Timesheets.Services.Requests.TaskExecutions;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class EmployeesController : ApiController
    {

        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger) => (_logger) = (logger);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllEmployeesQuery());

            return Ok(response);

        }
        
        [HttpGet("{EmployeeId}/Executions")]
        public async Task<IActionResult> GetEmployeeExecutions([FromRoute]GetEmployeeExecutionsQuery  request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }


        [HttpPut("Add")]
        public async Task<IActionResult> Add([FromBody]AddEmployeeCommand request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("{EmployeeId}/Task/{TaskId}/Execution/{TimeSpent}")]
        public async Task<IActionResult> AddEmployeeTaskExecution([FromRoute]AddEmployeeTaskExecutionCommand request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
