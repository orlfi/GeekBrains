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

namespace Timesheets.Services.Requests.Invoices
{
    public class AddInvoiceCommand : IRequest<InvoiceDto>
    {
        [FromRoute]
        public int ContractId { get; set; }

        [FromBody]
        public DateTime Date { get; set; }

        [FromBody]
        public decimal Total{ get; set; }

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
                    var invoice = _mapper.Map<Invoice>(request);
                    invoice.Contract = contract;
                    
                    decimal amount = 0;
                    foreach(var taskId in request.Tasks)
                    {
                        var task = await _tasksRepository.GetById(taskId);
                        invoice.Tasks.Add(task);
                        amount += task.Amount;
                    
                    }
                    
                    invoice.Total = amount*contract.HourCost;
                    invoice = await _invoicesRepository.Create(invoice);
                     await _contractsRepository.AddInvoice(invoice);
                    _mapper.Map<InvoiceDto>(invoice);
                }

                return null;
            }
        }
    }
}
