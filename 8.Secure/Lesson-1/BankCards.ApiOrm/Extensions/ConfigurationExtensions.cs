using BankCards.DAL.Context;
using Microsoft.EntityFrameworkCore;

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
                    options.UseNpgsql(config.GetConnectionString("postgres"));
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    break;
                default:
                    options.UseSqlServer(config.GetConnectionString("default"));
                    break;
            }
        });

        return services;
    }
}
