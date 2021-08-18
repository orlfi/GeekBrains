using System;
using System.Collections.Generic;
using Timesheets.DAL.Models;

namespace Timesheets.DAL
{
    public class TimesSheetsContext
    {
        public IList<Contract> Contracts { get; set; } = new List<Contract>();

        public IList<Customer> Customers { get; set; } = new List<Customer>();

        public IList<Employee> Employees { get; set; } = new List<Employee>();

        public IList<Task> Tasks { get; set; } = new List<Task>();

        public IList<Invoice> Invoices { get; set; } = new List<Invoice>();

        public IList<TaskExecution> TaskExecutions { get; set; } = new List<TaskExecution>();

        public void Initialize()
        {
            List<Customer> CustomersList = new List<Customer>
            {
                new Customer { Id = 0, Name = "ООО Рога и Копыта"},
                new Customer { Id = 1, Name = "АО Пивной дворик"}
            };
            CustomersList.ForEach((item) => Customers.Add(item));

            List<Employee> EmployeesList = new List<Employee>
            {
                new Employee { Id = 0, Name = "Иванов Иван Иванович"},
                new Employee { Id = 1, Name = "Петров Петр Петрович"},
                new Employee { Id = 2, Name = "Сидоров Сидр Сидорович"},
                new Employee { Id = 3, Name = "Смит Владимир"},
                new Employee { Id = 4, Name = "Монин Даниил"}
            };
            EmployeesList.ForEach((item) => Employees.Add(item));

            List<Task> TasksList = new List<Task>
            {
                new Task { Id = 0, Name = "Создание базы данных ", Amount = 20},
                new Task { Id = 1, Name = "Создание макета сайта", Amount = 30},
                new Task { Id = 2, Name = "Разработка API", Amount = 40, IsCompleted = true},
                new Task { Id = 3, Name = "Подготовка документации к проекту", Amount  =20},
                new Task { Id = 4, Name = "Создание клиентского приложения на React", Amount  =20},
                new Task { Id = 5, Name = "Создание приложения под Android", Amount  =20, IsCompleted = true}
            };
            TasksList.ForEach((item) => Tasks.Add(item));

            List<Contract> ContractsList = new List<Contract>
            {
                new Contract { Id = 0, Name = "Создание сайта", Number = "1/2021", HourCost =3000, Customer = Customers[0]},
                new Contract { Id = 1, Name = "Разработка учетной программы", Number = "2/2021", HourCost =3200, Customer = Customers[1]},
                new Contract { Id = 2, Name = "Создание сайта", Number = "2/2021", HourCost =3200, Customer = Customers[1]},
            };
            ContractsList.ForEach((item) => Contracts.Add(item));

            Invoices.Add(new Invoice()
                {
                    Id = 0,
                    Contract = Contracts[1],
                    Description = "Счет за выполненные работы по договору 2/2021",
                    Date = new DateTime(2021, 07, 31),
                    Total = 192000
                }
            );
            Contracts[1].Invoices.Add(Invoices[0]);
            Invoices[0].Tasks.Add(TasksList[2]);
            Invoices[0].Tasks.Add(TasksList[5]);

            Customers[0].Contracts.Add(Contracts[0]);
            Customers[1].Contracts.Add(Contracts[1]);
            Customers[1].Contracts.Add(Contracts[2]);

            List<TaskExecution> TaskExecutionsList = new List<TaskExecution>
            {
                new TaskExecution { Id = 0, Employee = Employees[3], Task = Tasks[2], TimeSpent = 10},
                new TaskExecution { Id = 1, Employee = Employees[4], Task = Tasks[2], TimeSpent = 20},
                new TaskExecution { Id = 2, Employee = Employees[1], Task = Tasks[2], TimeSpent = 10},
                new TaskExecution { Id = 3, Employee = Employees[0], Task = Tasks[5], TimeSpent = 20}
            };
            TaskExecutionsList.ForEach((item) => TaskExecutions.Add(item));

            Tasks[2].TaskExecutions.Add(TaskExecutions[0]);
            Employees[3].TaskExecutions.Add(TaskExecutions[0]);

            Tasks[2].TaskExecutions.Add(TaskExecutions[1]);
            Employees[4].TaskExecutions.Add(TaskExecutions[1]);

            Tasks[2].TaskExecutions.Add(TaskExecutions[2]);
            Employees[1].TaskExecutions.Add(TaskExecutions[2]);

            Tasks[5].TaskExecutions.Add(TaskExecutions[3]);
            Employees[0].TaskExecutions.Add(TaskExecutions[3]);
        }
    }
}