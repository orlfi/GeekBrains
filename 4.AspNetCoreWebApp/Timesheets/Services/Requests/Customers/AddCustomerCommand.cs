using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using AutoMapper;
using Timesheets.Services.Responses.Customers;

namespace Timesheets.Services.Requests.Customers
{
    public class AddCustomerCommand : IRequest<CustomerDto>
    {
        public string Name { get; set;}

        public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, CustomerDto>
        {
            private readonly ICustomersRepository _repository;
            private readonly IMapper _mapper;

            public  AddCustomerCommandHandler(ICustomersRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async Task<CustomerDto> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = await _repository.Create(_mapper.Map<Customer>(request));
                return _mapper.Map<CustomerDto>(customer);
            }
        }
    }
}
