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
    public class ContractsController : ControllerBase
    {
        private static readonly List<Contract> ContractsRepository = new List<Contract>
        {
            new Contract
            {
                Id = 0,
                ClientId = 0,
                Number = "666_2021",
                Date = new DateTime(2021,01,01),
                Name = "Договор разделки свиных ушей"
            },
            new Contract
            {
                Id = 1,
                ClientId = 0,
                Number = "555_2021",
                Date = new DateTime(2021,02,02),
                Name = "Договор отделения копыт"
            },
            new Contract
            {
                Id = 2,
                ClientId = 2,
                Number = "777_2021",
                Date = new DateTime(2021,03,03),
                Name = "Проведение вебинаров по C#"
            },
            new Contract
            {
                Id = 3,
                ClientId = 2,
                Number = "888_2021",
                Date = new DateTime(2021,04,04),
                Name = "Проведение вебинаров по JAVA"
            },
        };

        private readonly ILogger<ContractsController> _logger;

        public ContractsController(ILogger<ContractsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("");
            return Ok(ContractsRepository);
        }

        [HttpPost("modify")]
        public IActionResult Modify([FromBody] Contract Contract)
        {
            var entity = ContractsRepository.SingleOrDefault(item => item.Id == Contract.Id);
            if (entity == null)
                return BadRequest($"Contract with id {Contract.Id} is not found");

            entity.Name = Contract.Name;
            entity.Date = Contract.Date;
            entity.ClientId = Contract.ClientId;
            entity.Number = Contract.Number;
            return Ok();
        }

        [HttpPut("add")]
        public IActionResult Add([FromBody] Contract Contract)
        {
            if (ContractsRepository.Any(item => item.Name == Contract.Name.Trim()))
                return BadRequest($"The Contract with id {Contract.Id} is already exist");

            var maxId = ContractsRepository.Max(item => item.Id);
            Contract.Id = maxId + 1;
            ContractsRepository.Add(Contract);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var index = ContractsRepository.FindIndex(item => item.Id == id);
            if (index == -1)
                return BadRequest($"Contract with id {id} is not found");

            ContractsRepository.RemoveAt(index);
            return Ok();
        }
    }
}
