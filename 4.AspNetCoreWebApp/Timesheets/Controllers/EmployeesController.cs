using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheets.DAL.Models;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private static readonly List<Employee> EmployeesRepository = new List<Employee>
        {
            new Employee { Id = 0, Name = "Иванов Иван Иванович"},
            new Employee { Id = 1, Name = "Петров Петр Петрович"},
            new Employee { Id = 2, Name = "Сидоров Сидр Сидорович"},
            new Employee { Id = 3, Name = "Смит Владимир"},
            new Employee { Id = 3, Name = "Монин Даниил"}
        };

        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("");
            return Ok(EmployeesRepository);
        }

        [HttpPost("modify")]
        public IActionResult Modify([FromBody]Employee employee)
        {
            var entity = EmployeesRepository.SingleOrDefault(item => item.Id == employee.Id);
            if (entity == null)
                return BadRequest($"Employee with id {employee.Id} is not found");
            
            entity.Name = employee.Name;
            return Ok();
        }

        [HttpPut("add")]
        public IActionResult Add([FromBody]Employee employee)
        {
            if (EmployeesRepository.Any(item => item.Name == employee.Name.Trim()))
                return BadRequest($"The Employee with id {employee.Id} is already exist");

            var maxId = EmployeesRepository.Max(item => item.Id);
            employee.Id = maxId + 1;
            EmployeesRepository.Add(employee);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var index = EmployeesRepository.FindIndex(item => item.Id == id);
            if (index == -1)
                return BadRequest($"Employee with id {id} is not found");
            
            EmployeesRepository.RemoveAt(index);
            return Ok();
        }
    }
}
