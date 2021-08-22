using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Timesheets.DAL.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly TimesSheetsContext _db;

        public CustomersRepository(TimesSheetsContext db) => _db = db;

        public async Task<ICollection<Customer>> GetAll()
        {
            return await _db.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _db.Customers.SingleOrDefaultAsync(item => item.Id == id);
        }

        public async Task<Customer> Create(Customer entity)
        {
            try
            {
                await _db.Customers.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception($"Ошибка при создании нового покупателя: {ex.Message}");
            }
        }
    }
}