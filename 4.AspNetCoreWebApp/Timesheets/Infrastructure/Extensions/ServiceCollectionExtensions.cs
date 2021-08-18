using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Timesheets.Infrastructure.Mappers;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Repositories;
using Timesheets.DAL;
using Microsoft.EntityFrameworkCore;

namespace Timesheets.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TimesSheetsContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("Postgres"),
                    b=>b.MigrationsAssembly("Timesheets"));
            });
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<IContractsRepository, ContractsRepository>();
            services.AddScoped<IInvoicesRepository, InvoicesRepository>();
            services.AddScoped<ITaskExecutionsRepository, TaskExecutionsRepository>();
        }

        public static void ConfigureMappers(this IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}