
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Timesheets.DAL.Repositories
{
    public class ContractsRepository : IContractsRepository
    {
        private readonly TimesSheetsContext _db;

        public ContractsRepository(TimesSheetsContext db) => _db = db;

        public async Task<ICollection<Contract>> GetByCustomer(Customer customer)
        {
            var result = await System.Threading.Tasks.Task.Run<ICollection<Contract>>(() =>
            {
                return _db.Contracts.Where(item => item.Customer == customer).ToList();
            });

            return result;
        }

        public async Task<Contract> GetById(int id)
        {
            var result = await System.Threading.Tasks.Task.Run<Contract>(() =>
            {
                return _db.Contracts.SingleOrDefault(item => item.Id == id);
            });

            return result;
        }

        public async Task<Contract> Create(Contract entity)
        {
            var result = await System.Threading.Tasks.Task.Run<Contract>(() =>
            {
                entity.Id = _db.Contracts.Max(item => item.Id) + 1;
                _db.Contracts.Add(entity);
                return entity;
            });

            return result;
        }

        public async Task AddInvoice(Invoice entity)
        {
            await System.Threading.Tasks.Task.Run(() =>
           {
               var contract = _db.Contracts.FirstOrDefault(item => item.Id == entity.Contract.Id );
               if (contract != null)
               {
                   contract.Invoices.Add(entity);
               }
           });
        }
    }
}