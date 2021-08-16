using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using Timesheets.Services.Requests.Tasks;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly IMediator _mediator;

        public TasksController(ILogger<TasksController> logger, IMediator mediator) => (_logger, _mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllTasksQuery());

            return Ok(response);

        }

        [HttpPut("Add")]
        public async Task<IActionResult> Add([FromBody]AddTaskCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }


        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody]UpdateTaskCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteTaskCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
