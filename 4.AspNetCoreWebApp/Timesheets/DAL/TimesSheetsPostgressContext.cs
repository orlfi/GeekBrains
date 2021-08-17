using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Timesheets.DAL.Models;

namespace Timesheets.DAL
{
    public sealed class TimesSheetsPostgressContext : DbContext
    {
        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<TaskExecution> TaskExecutions { get; set; }

        public TimesSheetsPostgressContext(DbContextOptions<TimesSheetsPostgressContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //InitializeData(modelBuilder);
        }

        private void InitializeData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
            new Customer[] 
            {
                new Customer { Id = 0, Name = "ООО Рога и Копыта"},
                new Customer { Id = 1, Name = "АО Пивной дворик"}
            });
        }
    }
}