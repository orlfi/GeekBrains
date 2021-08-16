using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Services.Responses.Contracts;
using Timesheets.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;

namespace Timesheets.Services.Requests.Customers
{
    public class GetCustomerContractsQuery : IRequest<ContractsResponse>
    {
        [FromRoute]
        public int CustomerId { get; set; }

        public class GetCustomerContractsQueryHandler : IRequestHandler<GetCustomerContractsQuery, ContractsResponse>
        {

            private readonly IContractsRepository _contractsRepository;
            private readonly ICustomersRepository _customersRepository;
            private readonly IMapper _mapper;

            public GetCustomerContractsQueryHandler(IContractsRepository contractsRepository, ICustomersRepository customersRepository, IMapper mapper) =>
                (_contractsRepository, _customersRepository, _mapper) = (contractsRepository, customersRepository, mapper);

            public async Task<ContractsResponse> Handle(GetCustomerContractsQuery request, CancellationToken cancellationToken)
            {
                var customer = await _customersRepository.GetById(request.CustomerId);
                if (customer != null)
                {
                    var contracts = await _contractsRepository.GetByCustomer(customer);

                    var response = new ContractsResponse();
                    response.Contracts.AddRange(_mapper.Map<List<ContractDto>>(contracts));

                    return response;
                }
                else
                    return null;
            }
        }
    }
}
