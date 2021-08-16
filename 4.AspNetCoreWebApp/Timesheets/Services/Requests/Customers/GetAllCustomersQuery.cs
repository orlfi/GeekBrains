using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timesheets.Services.Responses.Customers;
using Timesheets.DAL.Interfaces;

using AutoMapper;

namespace  Timesheets.Services.Requests.Customers
{
    public class GetAllCustomersQuery : IRequest<CustomersResponse>
    {
        public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, CustomersResponse>
        {
            private readonly ICustomersRepository _repository;
            private readonly IMapper _mapper;

            public GetAllCustomersQueryHandler(ICustomersRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<CustomersResponse> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
            {
                var customers = await _repository.GetAll();

                var response = new CustomersResponse();

                response.Customers.AddRange(_mapper.Map<List<CustomerDto>>(customers));

                return response;
            }
        }
    }
}
