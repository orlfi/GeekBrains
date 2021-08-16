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
    public class UpdateCustomerCommand : IRequest<CustomerDto>
    {
        public int Id { get; set;}
        public string Name { get; set;}

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
        {
            private readonly ICustomersRepository _repository;
            private readonly IMapper _mapper;

            public  UpdateCustomerCommandHandler(ICustomersRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = await _repository.Update(_mapper.Map<Customer>(request));
                return _mapper.Map<CustomerDto>(customer);
            }
        }
    }
}
