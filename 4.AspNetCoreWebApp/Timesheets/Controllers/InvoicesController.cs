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
    public class InvoicesController : ControllerBase
    {
        private static readonly List<Invoice> InvoicesRepository = new List<Invoice>
        {
            new Invoice
            {
                ContractId = 0,
                Total = 1000.25M,
                Date = new DateTime(2021,08,01),
                Description = "За отделение партии ушей"
            },
            new Invoice
            {
                ContractId = 0,
                Total = 500.5M,
                Date = new DateTime(2021,08,05),
                Description = "За упаковку ушей в коробку"
            },
            new Invoice
            {
                ContractId = 2,
                Total = 2.0M,
                Date = new DateTime(2021,08,03),
                Description = "Чтение 2 лекции курса ASP Web App"
            },
            new Invoice
            {
                ContractId = 2,
                Total = 3.0M,
                Date = new DateTime(2021,08,10),
                Description = "Чтение 3 лекции курса ASP Web App"
            },
            new Invoice
            {
                ContractId = 2,
                Total = 1_000_000.0M,
                Date = new DateTime(2021,08,12),
                Description = "Проведение вебинара самурая"
            }
        };

        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(ILogger<InvoicesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("");
            return Ok(InvoicesRepository);
        }

        [HttpPost("modify")]
        public IActionResult Modify([FromBody] Invoice Invoice)
        {
            var entity = InvoicesRepository.SingleOrDefault(item => item.ContractId == Invoice.ContractId && item.Date == Invoice.Date);
            if (entity == null)
                return BadRequest($"Invoice with ContractId={Invoice.ContractId} and Date={Invoice.Date} is not found");

            entity.Description = Invoice.Description;
            entity.Total = Invoice.Total;
            return Ok();
        }

        [HttpPut("add")]
        public IActionResult Add([FromBody] Invoice Invoice)
        {
            if (InvoicesRepository.Any(item => item.ContractId == Invoice.ContractId && item.Date == Invoice.Date))
                return BadRequest($"The Invoice with ContractId={Invoice.ContractId} and Date={Invoice.Date} is already exist");

            InvoicesRepository.Add(Invoice);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromBody] Invoice Invoice)
        {
            var index = InvoicesRepository.FindIndex(item => item.ContractId == Invoice.ContractId && item.Date == Invoice.Date);
            if (index == -1)
                return BadRequest($"Invoice with ContractId={Invoice.ContractId} and Date={Invoice.Date} is not found");

            InvoicesRepository.RemoveAt(index);
            return Ok();
        }
    }
}
