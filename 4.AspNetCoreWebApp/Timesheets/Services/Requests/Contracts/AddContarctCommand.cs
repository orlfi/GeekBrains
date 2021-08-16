using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Timesheets.DAL.Repositories;
using AutoMapper;
using Timesheets.Services.Responses.Contracts;

namespace Timesheets.Services.Requests.Contracts
{
    public class AddContractCommand : IRequest<ContractDto>
    {
        [FromRoute]
        public int CustomerId { get; set; }

        [FromBody]
        public string Name { get; set; }
        
        [FromBody]
        public string Number { get; set; }

        [FromBody]
        public decimal HourCost { get; set;}

        public class AddContractCommandHandler : IRequestHandler<AddContractCommand, ContractDto>
        {
            private readonly IContractsRepository _contractsRepository;
            private readonly ICustomersRepository _customersRepository;
            private readonly IMapper _mapper;

            public AddContractCommandHandler(IContractsRepository contractsRepository, ICustomersRepository customersRepository, IMapper mapper) => 
                (_contractsRepository, _customersRepository, _mapper) = (contractsRepository, customersRepository, mapper);

            public async Task<ContractDto> Handle(AddContractCommand request, CancellationToken cancellationToken)
            {
                Customer customer = await _customersRepository.GetById(request.CustomerId);
                if (customer != null)
                {
                    var contract = _mapper.Map<Contract>(request);
                    contract.Customer = customer;
                    contract = await _contractsRepository.Create(contract);
                    await _customersRepository.AddContract(contract);
                    _mapper.Map<ContractDto>(contract);
                }

                return null;
            }
        }
    }
}
