
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;

namespace Timesheets.DAL.Repositories.Memory
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly TimesSheetsMemoryContext _db;

        public CustomersRepository(TimesSheetsMemoryContext db) => _db = db;

        public async Task<ICollection<Customer>> GetAll()
        {
            var result = await System.Threading.Tasks.Task.Run<ICollection<Customer>>(() =>
            {
                return _db.Customers;
            });

            return result;
        }

        public async Task<Customer> GetById(int id)
        {
            var result = await System.Threading.Tasks.Task.Run<Customer>(() =>
            {
                return _db.Customers.SingleOrDefault(item => item.Id == id);
            });

            return result;
        }

        public async Task<Customer> Create(Customer entity)
        {
            var result = await System.Threading.Tasks.Task.Run<Customer>(() =>
            {
                entity.Id = _db.Customers.Max(item => item.Id) + 1;
                _db.Customers.Add(entity);
                return entity;
            });

            return result;
        }

        public async System.Threading.Tasks.Task AddContract(Contract entity)
        {
           await System.Threading.Tasks.Task.Run(() =>
           {
               var customer = _db.Customers.FirstOrDefault(item => item.Id == entity.Customer.Id);
               if (customer != null)
               {
                   customer.Contracts.Add(entity);
               }
           });
        }
    }
}