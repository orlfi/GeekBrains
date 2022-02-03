using BankCards.DAL.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;

namespace BankCards.ApiOrm.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<BankContext>(options =>
        {
            var useDbOption = config["UseDb"]?.ToLower();

            switch (useDbOption)
            {
                case "postgres":
                    NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder(config.GetConnectionString("postgres"));
                    connectionStringBuilder.Password = config["DbPassword"];
                    options.UseNpgsql(connectionStringBuilder.ConnectionString,
                        x => x.MigrationsAssembly("BankCards.PostgresMigrations"));
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    break;
                default:
                    options.UseSqlServer(config.GetConnectionString("default"),
                        x => x.MigrationsAssembly("BankCards.MsSqlServerMigrations"));
                    break;
            }
        });

        return services;
    }
}
