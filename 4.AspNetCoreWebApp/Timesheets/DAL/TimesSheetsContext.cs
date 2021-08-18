using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Timesheets.DAL.Models;

namespace Timesheets.DAL
{
    public sealed class TimesSheetsContext : DbContext
    {
        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<TaskExecution> TaskExecutions { get; set; }

        public TimesSheetsContext(DbContextOptions<TimesSheetsContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //InitializeData(modelBuilder);
        }

        // private void InitializeData(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Customer>().HasData(new Customer[]
        //     {
        //         new Customer { Id = 1, Name = "ООО Рога и Копыта"},
        //         new Customer { Id = 2, Name = "АО Пивной дворик"}
        //     });

        //     modelBuilder.Entity<Employee>().HasData(new Employee[]
        //     {
        //         new Employee { Id = 1, Name = "Иванов Иван Иванович"},
        //         new Employee { Id = 2, Name = "Петров Петр Петрович"},
        //         new Employee { Id = 3, Name = "Сидоров Сидр Сидорович"},
        //         new Employee { Id = 4, Name = "Смит Владимир"},
        //         new Employee { Id = 5, Name = "Монин Даниил"}
        //     });

        //     modelBuilder.Entity<Contract>().HasData(new Contract[]
        //     {
        //         new Contract { Id = 1, Name = "Создание сайта", Number = "1/2021", HourCost =3000, CustomerId = 1},
        //         new Contract { Id = 2, Name = "Разработка учетной программы", Number = "2/2021", HourCost =3200, CustomerId = 2},
        //         new Contract { Id = 3, Name = "Создание сайта", Number = "2/2021", HourCost =3200, CustomerId = 2}
        //     });

        //     modelBuilder.Entity<Invoice>().HasData(new Invoice[]
        //     {
        //         new Invoice()
        //         {
        //             Id = 1,
        //             ContractId = 2,
        //             Description = "Счет за выполненные работы по договору 2/2021",
        //             Date = new DateTime(2021, 07, 31),
        //             Total = 192000
        //         }
        //     });

        //     modelBuilder.Entity<Task>().HasData(new Task[]
        //     {
        //         new Task { Id = 1, Name = "Создание базы данных ", Amount = 20},
        //         new Task { Id = 2, Name = "Создание макета сайта", Amount = 30},
        //         new Task { Id = 3, Name = "Разработка API", Amount = 40, IsCompleted = true, InvoiceId = 1},
        //         new Task { Id = 4, Name = "Подготовка документации к проекту", Amount  =20},
        //         new Task { Id = 5, Name = "Создание клиентского приложения на React", Amount  =20},
        //         new Task { Id = 6, Name = "Создание приложения под Android", Amount  =20, IsCompleted = true, InvoiceId = 1}
        //     });

        //     modelBuilder.Entity<TaskExecution>().HasData(new TaskExecution[]
        //     {

        //         new TaskExecution { Id = 1, EmployeeId = 4, TaskId = 3, TimeSpent = 10},
        //         new TaskExecution { Id = 2, EmployeeId = 5, TaskId = 3, TimeSpent = 20},
        //         new TaskExecution { Id = 3, EmployeeId = 2, TaskId = 3, TimeSpent = 10},
        //         new TaskExecution { Id = 4, EmployeeId = 1, TaskId = 6, TimeSpent = 20}
        //     });
        // }
    }
}