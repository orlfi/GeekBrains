using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using MediatR;
using Timesheets.Services.Responses.Customers;
using Timesheets.Controllers;
using Timesheets.Services.Requests.Customers;
using Timesheets.Services.Responses.Contracts;

namespace MetricsManagerTests
{
    public class CustomersControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<CustomersController>> _mockLogger;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockLogger = new Mock<ILogger<CustomersController>>();
            _mockMediator = new Mock<IMediator>();
            _controller = new CustomersController(_mockLogger.Object, _mockMediator.Object);
        }

        [Fact]
        public async Task GetAll_ReturnOk()
        {
            var customersResponse = new CustomersResponse()
                {
                    Customers = new List<CustomerDto>()
                    {
                       new CustomerDto()
                       {
                           Id = 1,
                           Name = "ООО Рога и Копыта"
                       },
                       new CustomerDto()
                       {
                           Id = 2,
                           Name = "АО Пивной дворик"
                       }
                    }
                };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetAllCustomersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customersResponse);

            var result = await _controller.GetAll();
            var resultValue = ((OkObjectResult)result).Value as CustomersResponse;

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<GetAllCustomersQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(2, resultValue.Customers.Count);
            Assert.Equal(1, resultValue.Customers[0].Id);
            Assert.Equal("ООО Рога и Копыта", resultValue.Customers[0].Name);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task GetCustomerContracts_ReturnOk()
        {
            var query = new GetCustomerContractsQuery() 
            {
                CustomerId = 1
            };
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetCustomerContractsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContractsResponse()
                {
                    Contracts = new List<ContractDto>()
                    {
                        new ContractDto()
                        {
                            Id = 1,
                            Name = "Test",
                            HourCost = 10,
                            Number = "123"
                        }
                    }
                });

            var result = await _controller.GetCustomerContracts(query);
            var resultValue = ((OkObjectResult)result).Value as ContractsResponse;

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<GetCustomerContractsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Single(resultValue.Contracts);
            Assert.Equal(1, resultValue.Contracts[0].Id);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
