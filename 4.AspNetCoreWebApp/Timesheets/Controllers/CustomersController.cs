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
    public class CustomersController : ControllerBase
    {
        private static readonly List<Customer> ClientsRepository = new List<Customer>
        {
            new Customer { Id = 0, Name = "ООО Рога и Копыта"},
            new Customer { Id = 1, Name = "МММ"},
            new Customer { Id = 2, Name = "GeekBrains"}
        };

        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("");
            return Ok(ClientsRepository);
        }

        [HttpPost("modify")]
        public IActionResult Modify([FromBody]Customer Client)
        {
            var entity = ClientsRepository.SingleOrDefault(item => item.Id == Client.Id);
            if (entity == null)
                return BadRequest($"Client with id {Client.Id} is not found");
            
            entity.Name = Client.Name;
            return Ok();
        }

        [HttpPut("add")]
        public IActionResult Add([FromBody]Customer Client)
        {
            if (ClientsRepository.Any(item => item.Name == Client.Name.Trim()))
                return BadRequest($"The Client with id {Client.Id} is already exist");

            var maxId = ClientsRepository.Max(item => item.Id);
            Client.Id = maxId + 1;
            ClientsRepository.Add(Client);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var index = ClientsRepository.FindIndex(item => item.Id == id);
            if (index == -1)
                return BadRequest($"Client with id {id} is not found");
            
            ClientsRepository.RemoveAt(index);
            return Ok();
        }
    }
}
