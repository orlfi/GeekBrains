
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Timesheets.DAL.Repositories.Postgress
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly TimesSheetsPostgressContext _db;

        public CustomersRepository(TimesSheetsPostgressContext db) => _db = db;

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
            await _db.Customers.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async System.Threading.Tasks.Task AddContract(Contract entity)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(item => item.Id == entity.Customer.Id);
            if (customer != null)
            {
                customer.Contracts.Add(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}