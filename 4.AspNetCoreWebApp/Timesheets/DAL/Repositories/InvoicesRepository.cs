using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;

namespace Timesheets.DAL.Repositories
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly TimesSheetsContext _db;

        public InvoicesRepository(TimesSheetsContext db) => _db = db;

        public async Task<ICollection<Invoice>> GetContractInvoicesByPeriod(Contract contract, DateTime dateFrom, DateTime dateTo)
        {
            var result = await System.Threading.Tasks.Task.Run<ICollection<Invoice>>(() =>
            {
                return _db.Invoices.Where(item => item.Contract == contract && item.Date >= dateFrom && item.Date < dateTo).ToList();
            });

            return result;
        }

        public async Task<Invoice> Create(Invoice entity)
        {
            var result = await System.Threading.Tasks.Task.Run<Invoice>(() =>
            {
                entity.Id = _db.Invoices.Max(item => item.Id) + 1;
                _db.Invoices.Add(entity);
                return entity;
            });

            return result;
        }
    }
}