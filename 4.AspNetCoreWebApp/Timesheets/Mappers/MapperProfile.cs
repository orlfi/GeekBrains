using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Timesheets.DAL.Models;
using Timesheets.Services.Responses.Customers;
using Timesheets.Services.Requests.Customers;
using Timesheets.Services.Responses.Employees;
using Timesheets.Services.Responses.Contracts;
using Timesheets.Services.Responses.Invoices;
using Timesheets.Services.Requests.Employees;
using Timesheets.Services.Requests.Contracts;
using Timesheets.Services.Requests.Invoices;
using Timesheets.Services.Responses.Tasks;
using Timesheets.Services.Requests.Tasks;
using Timesheets.Services.Responses.TaskExecutions;
using Timesheets.Services.Requests.TaskExecutions;

using Task = Timesheets.DAL.Models.Task;

namespace Timesheets.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();
            CreateMap<Customer, CustomerDto>();

            CreateMap<AddEmployeeCommand, Employee>();
            CreateMap<UpdateEmployeeCommand, Employee>();
            CreateMap<Employee, EmployeeDto>();

            CreateMap<AddTaskCommand, Task>();
            CreateMap<UpdateTaskCommand, Task>();
            CreateMap<Task, TaskDto>();

            CreateMap<AddContractCommand, Contract>();
            CreateMap<Contract, ContractDto>();

            CreateMap<AddInvoiceCommand, Invoice>();
            CreateMap<Invoice, InvoiceDto>();

            CreateMap<AddEmployeeTaskExecutionCommand, TaskExecution>();
            CreateMap<TaskExecution, TaskExecutionDto>();
        }
    }
}
