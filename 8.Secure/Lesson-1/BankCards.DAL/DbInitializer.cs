using BankCards.DAL.Context;
using BankCards.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankCards.DAL;

public class DbInitializer : IDbInitializer
{
    private readonly BankContext _db;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(BankContext db, ILogger<DbInitializer> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task InitializeAsync(CancellationToken cancel = default)
    {
        try
        {
            var pendingMigrations = await _db.Database.GetPendingMigrationsAsync(cancel).ConfigureAwait(false);
            if (pendingMigrations.Any())
            {
                _logger.LogInformation("Выполняется миграция БД...");
                await _db.Database.MigrateAsync(cancel).ConfigureAwait(false);
                _logger.LogInformation("Миграция БД выполнена");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка инициализации");
            throw;
        }
    }
}
