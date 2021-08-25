using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Timesheets.DAL.Repositories;
using AutoMapper;
using Timesheets.Services.Responses.Invoices;
using Timesheets.Domain.Aggregates.InvoiceAggregate;

namespace Timesheets.Services.Requests.Invoices
{
    public class AddInvoiceCommand : IRequest<InvoiceDto>
    {
        [FromRoute]
        public int ContractId { get; set; }

        [FromBody]
        public DateTime Date { get; set; }

        [FromBody]
        public string Description { get; set; }

        [FromBody]
        public List<int> Tasks { get; set; } = new List<int>();

        public class AddInvoiceCommandHandler : IRequestHandler<AddInvoiceCommand, InvoiceDto>
        {
            private readonly IInvoicesRepository _invoicesRepository;
            private readonly IContractsRepository _contractsRepository;
            private readonly ITasksRepository _tasksRepository;
            private readonly IMapper _mapper;

            public AddInvoiceCommandHandler(IInvoicesRepository invoicesRepository, IContractsRepository contractsRepository, ITasksRepository tasksRepository, IMapper mapper) => 
                (_invoicesRepository, _contractsRepository, _tasksRepository, _mapper) = (invoicesRepository, contractsRepository, tasksRepository, mapper);

            public async Task<InvoiceDto> Handle(AddInvoiceCommand request, CancellationToken cancellationToken)
            {
                Contract contract = await _contractsRepository.GetById(request.ContractId);
                if (contract != null)
                {
                    var invoiceAggregate = InvoiceAggregate.Create(contract.Id, request.Date, request.Description);
                    
                    var tasks = await _tasksRepository.GetByIdList(request.Tasks);
                    invoiceAggregate.IncludeTasks(tasks, contract.HourCost);

                    var invoice = await _invoicesRepository.Create(invoiceAggregate);
                    _mapper.Map<InvoiceDto>(invoice);
                }

                return null;
            }
        }
    }
}
