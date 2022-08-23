using ClinicService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using NLog.Web;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using ClinicService.DAL.Interfaces;
using ClinicService.DAL;
using ClinicService.Services;

namespace ClinicService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(IPAddress.Any, 5001, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });

        builder.Services.AddDbContext<ClinicServiceDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddGrpc();

        #region Configure logging service

        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
            logging.RequestHeaders.Add("Authorization");
            logging.RequestHeaders.Add("X-Real-IP");
            logging.RequestHeaders.Add("X-Forwarded-For");
        });


        builder.Host.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();

        }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

        #endregion

        #region Configure Repository Services

        builder.Services.AddScoped<IPetRepository, PetRepository>();
        builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();

        #endregion

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseWhen( // ������, ��������� ��������� � .NET 7
            ctx => ctx.Request.ContentType != "application/grpc",
            builder =>
            {
                builder.UseHttpLogging();
            }
        );

        app.UseAuthorization();


        app.MapControllers();
        app.UseRouting();
        app.UseEndpoints(endpoints =>  // 2
        {
            // Communication with gRPC endpoints must be made through a gRPC client.
            // To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909
            endpoints.MapGrpcService<ClientService>();
            endpoints.MapGrpcService<PetService>();
        });

        app.Run();
    }
}