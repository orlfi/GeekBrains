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
    public class EmployeesController : ControllerBase
    {

        private readonly ILogger<EmployeesController> _logger;
        private readonly IMediator _mediator;

        public EmployeesController(ILogger<EmployeesController> logger, IMediator mediator) => (_logger, _mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllEmployeesQuery());

            return Ok(response);

        }
        
        [HttpGet("{EmployeeId}/Executions")]
        public async Task<IActionResult> GetEmployeeExecutions([FromRoute]GetEmployeeExecutionsQuery  request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }


        [HttpPut("Add")]
        public async Task<IActionResult> Add([FromBody]AddEmployeeCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("{EmployeeId}/Task/{TaskId}/Execution/{TimeSpent}")]
        public async Task<IActionResult> AddEmployeeTaskExecution([FromRoute]AddEmployeeTaskExecutionCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody]UpdateEmployeeCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteEmployeeCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
