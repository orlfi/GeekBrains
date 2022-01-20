using BankCards.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BankCards.ApiOrm.Extensions;

public static class MigrationExtensions
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<BankContext>();

        appContext.Database.Migrate();

        return host;
    }
}