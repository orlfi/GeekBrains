using System;
using System.Collections.Generic;
using System.Linq;
using Timesheets.DAL.Models;
using Timesheets.Exceptions;

namespace Timesheets.Domain.Aggregates.InvoiceAggregate
{
    public class InvoiceAggregate: Invoice
    {
        private InvoiceAggregate(){}

        public static InvoiceAggregate Create(int contractId, DateTime date, string description)
        {
            return new InvoiceAggregate()
            {
                ContractId = contractId,
                Date = date,
                Description = description
            };
        }

        public void IncludeTasks(IEnumerable<Task> tasks, decimal hourCost)
        {
            Tasks = new List<Task>();
            foreach(var item in tasks)
            {
                if (item.InvoiceId.HasValue)
                {
                    throw new TaskIsPaidException(item.Id);
                }
                Tasks.Add(item);
            }

            CalculateSum(hourCost);
        }

        private void CalculateSum(decimal hourCost)
        {
            Total = Tasks.Sum(item => item.Amount * hourCost);
        }
    }
}