using System;
using System.Collections.Generic;
using Xunit;
using Timesheets.Domain.Aggregates.InvoiceAggregate;
using Timesheets.Exceptions;

namespace MetricsManagerTests
{
    public class InvoiceAggregateTests
    {
        private readonly InvoiceAggregate _invoiceAggregate;

        public InvoiceAggregateTests()
        {
            _invoiceAggregate = InvoiceAggregate.Create(1, DateTime.Today, "Test" );
        }

        [Fact]
        public async void IncludeTasks_And_CalculateSum_Test()
        {
            var tasks = new List<Timesheets.DAL.Models.Task>() 
            {
                new Timesheets.DAL.Models.Task() 
                {
                    Id = 1,
                    IsCompleted = true,
                    Name = "Test1",
                    Amount = 10,
                },
                new Timesheets.DAL.Models.Task() 
                {
                    Id = 2,
                    IsCompleted = true,
                    Name = "Test2",
                    Amount = 20,
                }
            };

            _invoiceAggregate.IncludeTasks(tasks, 10);

            Assert.Equal(2, _invoiceAggregate.Tasks.Count);
            Assert.Equal(300, _invoiceAggregate.Total);
        }

        [Fact]
        public async void IncludeTasks_Error_When_Task_Is_Paid()
        {
            var tasks = new List<Timesheets.DAL.Models.Task>() 
            {
                new Timesheets.DAL.Models.Task() 
                {
                    Id = 1,
                    IsCompleted = true,
                    Name = "Test1",
                    Amount = 10,
                },
                new Timesheets.DAL.Models.Task() 
                {
                    Id = 2,
                    InvoiceId = 2,
                    IsCompleted = true,
                    Name = "Test2",
                    Amount = 20,
                }
            };

            Action action = () => _invoiceAggregate.IncludeTasks(tasks, 10);

            Assert.Throws<TaskIsPaidException>(action);
        }
    }
}
