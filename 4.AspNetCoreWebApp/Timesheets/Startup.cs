using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MediatR;
using Timesheets.Filters;
using Timesheets.DAL;
using Timesheets.DAL.Interfaces;
using Timesheets.DAL.Models;
using Timesheets.Infrastructure.Extensions;
using Timesheets.Services.Authentication;
namespace Timesheets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();	
            services.AddCors();	

            services.AddControllers((options) =>
            {
                options.Filters.Add(typeof(MyActionFilter));
            });

            services.ConfigureAuthentication();
            services.ConfigureSwagger();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.ConfigureDbContext(Configuration);
            services.ConfigureRepositories();
            services.ConfigureMappers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Timesheets v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
