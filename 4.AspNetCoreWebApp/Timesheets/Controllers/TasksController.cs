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
    public class TasksController : ApiController
    {
        private readonly ILogger<TasksController> _logger;

        public TasksController(ILogger<TasksController> logger) => (_logger) = (logger);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllTasksQuery());

            return Ok(response);

        }

        [HttpPut("Add")]
        public async Task<IActionResult> Add([FromBody]AddTaskCommand request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
