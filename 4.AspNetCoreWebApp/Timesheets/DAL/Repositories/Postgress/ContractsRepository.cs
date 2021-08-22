using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Task = System.Threading.Tasks.Task;
using Microsoft.EntityFrameworkCore;

namespace Timesheets.DAL.Repositories
{
    public class ContractsRepository : IContractsRepository
    {
        private readonly TimesSheetsContext _db;

        public ContractsRepository(TimesSheetsContext db) => _db = db;

        public async Task<ICollection<Contract>> GetByCustomer(Customer customer)
        {
            return await _db.Contracts.Where(item => item.Customer == customer).ToListAsync();
        }

        public async Task<Contract> GetById(int id)
        {
            return await _db.Contracts.SingleOrDefaultAsync(item => item.Id == id);
        }

        public async Task<Contract> Create(Contract entity)
        {
            try
            {
                await _db.Contracts.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception($"Ошибка при создании нового договора: {ex.Message}");
            }
        }
    }
}