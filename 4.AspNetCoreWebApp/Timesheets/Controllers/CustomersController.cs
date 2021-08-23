using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using Timesheets.Services.Requests.Customers;
using Timesheets.Services.Requests.Contracts;
using Timesheets.Services.Requests.Invoices;
using Microsoft.AspNetCore.Authorization;

namespace Timesheets.Controllers
{
    [ApiController]
   // [Authorize]
    [Route("Api/[controller]")]
    public class CustomersController : ApiController
    {
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger) => (_logger) = (logger);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllCustomersQuery());

            return Ok(response);

        }
        
        [HttpGet("{CustomerId}/Contracts")]
        public async Task<IActionResult> GetCustomerContracts([FromRoute]GetCustomerContractsQuery request)
        {
            //var response = await Mediator.Send(new GetCustomerContractsQuery() {CustomerId = customerId});
            var response = await Mediator.Send(request);

            return Ok(response);
        }
        
        [HttpGet("Contract/{ContractId}/Invoices/From/{DateFrom}/To/{DateTo}")]
        public async Task<IActionResult> GetContractInvoices([FromRoute]int ContractId, [FromRoute] DateTime DateFrom, [FromRoute] DateTime DateTo)
        {
            var response = await Mediator.Send(new GetContractInvoicesByPeriodQuery() 
            {
                ContractId = ContractId,
                DateFrom = DateFrom,
                DateTo = DateTo
            });

            return Ok(response);
        }

        [HttpPut("Add")]
        public async Task<IActionResult> Add([FromBody]AddCustomerCommand request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("{CustomerId}/Contract")]
        public async Task<IActionResult> AddContract(AddContractCommand request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("Contract/{ContractId}/Invoice")]
        public async Task<IActionResult> AddInvoice(AddInvoiceCommand request)
        {
            var response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
