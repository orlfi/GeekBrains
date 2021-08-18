using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Services.Responses.Invoices;
using Timesheets.DAL.Interfaces;

using AutoMapper;

namespace Timesheets.Services.Requests.Customers
{
    public class GetContractInvoicesByPeriodQuery : IRequest<InvoicesResponse>
    {
        public int ContractId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public class GetContractInvoicesByPeriodQueryHandler : IRequestHandler<GetContractInvoicesByPeriodQuery, InvoicesResponse>
        {

            private readonly IInvoicesRepository _invoicesRepository;
            private readonly IContractsRepository _contractsRepository;
            private readonly IMapper _mapper;

            public GetContractInvoicesByPeriodQueryHandler(IInvoicesRepository invoicesRepository, IContractsRepository contractsRepository, IMapper mapper) =>
                (_invoicesRepository, _contractsRepository, _mapper) = (invoicesRepository, contractsRepository, mapper);

            public async Task<InvoicesResponse> Handle(GetContractInvoicesByPeriodQuery request, CancellationToken cancellationToken)
            {
                    var invoices = await _invoicesRepository.GetContractInvoicesByPeriod(request.ContractId, request.DateFrom, request.DateTo);

                    var response = new InvoicesResponse();
                    response.Invoices.AddRange(_mapper.Map<List<InvoiceDto>>(invoices));

                    return response;
            }
        }
    }
}
