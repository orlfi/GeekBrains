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
    public class DeleteCustomerCommand : IRequest
    {
        public int Id { get; set;}

        public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
        {
            private readonly ICustomersRepository _repository;
            private readonly IMapper _mapper;

            public  DeleteCustomerCommandHandler(ICustomersRepository repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

            public async System.Threading.Tasks.Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                await _repository.Delete(request.Id);
                return Unit.Value;
            }
        }
    }
}
