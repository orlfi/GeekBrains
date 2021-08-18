using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Timesheets.DAL.Repositories
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly TimesSheetsContext _db;

        public InvoicesRepository(TimesSheetsContext db) => _db = db;

        public async Task<ICollection<Invoice>> GetContractInvoicesByPeriod(int contractId, DateTime dateFrom, DateTime dateTo)
        {
            return await _db.Invoices.Where(item => item.Contract.Id == contractId && item.Date >= dateFrom && item.Date < dateTo).ToListAsync();
        }

        public async Task<Invoice> Create(Invoice entity)
        {
            try
            {
                await _db.Invoices.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception($"Ошибка при создании нового счета: {ex.Message}");
            }
        }
    }
}