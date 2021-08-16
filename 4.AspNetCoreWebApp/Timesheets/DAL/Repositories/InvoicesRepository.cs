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

        // public async Task<Invoice> Update(Invoice Invoice)
        // {
        //     await System.Threading.Tasks.Task.Run(() =>
        //     {
        //         var updatedInvoice = _db.Invoices.SingleOrDefault(item => item.Id == Invoice.Id);
        //         if (updatedInvoice != null)
        //         {
        //             var index = _db.Invoices.IndexOf(updatedInvoice);
        //             _db.Invoices[index] = Invoice;
        //         }
        //     });

        //     return Invoice;
        // }

        // public async System.Threading.Tasks.Task Delete(int id)
        // {
        //     await System.Threading.Tasks.Task.Run(() =>
        //    {
        //        var Invoice = _db.Invoices.FirstOrDefault(item => item.Id == id);
        //        if (Invoice != null)
        //        {
        //            _db.Invoices.Remove(Invoice);
        //        }
        //    });
        // }
    }
}