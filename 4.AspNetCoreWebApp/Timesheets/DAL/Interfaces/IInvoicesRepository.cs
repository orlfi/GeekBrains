using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.DAL.Models;

namespace Timesheets.DAL.Interfaces
{
    public interface IInvoicesRepository
    {
        Task<ICollection<Invoice>> GetContractInvoicesByPeriod(Contract contract, DateTime dateFrom, DateTime dateTo);

        Task<Invoice> Create(Invoice entity);
    }
}