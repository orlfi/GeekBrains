using BankCards.DAL.Configuration;
using BankCards.DAL.Context;
using BankCards.Domain.Account;
using BankCards.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BankCards.DAL;

public class DbInitializer : IDbInitializer
{
    private readonly BankContext _db;
    private readonly ILogger<DbInitializer> _logger;
    private readonly UserManager<AppUser> _userManager;

    public DbInitializer(BankContext db, UserManager<AppUser> userManager, ILogger<DbInitializer> logger)
    {
        _db = db;
        _logger = logger;
        _userManager= userManager;
    }

    public async Task<bool> DeleteAsync(CancellationToken Cancel = default)
    {
        var timer = Stopwatch.StartNew();
        Cancel.ThrowIfCancellationRequested();

        try
        {
            _logger.LogInformation("Удаление БД...");

            var result = await _db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false);

            _logger.LogInformation("БД удалена успешно за {0}c", timer.Elapsed.TotalSeconds);

            return result;
        }
        catch (OperationCanceledException e)
        {
            _logger.LogInformation("Операция удаления БД была прервана");
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при удалении БД: {0}", e.Message);
            throw;
        }
    }

    public async Task InitializeAsync(bool removeFirst = false, bool initializeDatabaseWithTestData = false, CancellationToken cancel = default)
    {
        try
        {
            if (removeFirst)
                await DeleteAsync(cancel).ConfigureAwait(false);

            var pendingMigrations = await _db.Database.GetPendingMigrationsAsync(cancel).ConfigureAwait(false);
            if (pendingMigrations.Any())
            {
                _logger.LogInformation("Database migration is in progress...");
                await _db.Database.MigrateAsync(cancel).ConfigureAwait(false);
                _logger.LogInformation("Database migration is completed");
            }

            if (initializeDatabaseWithTestData)
                await DataSeed.SeedDataAsync(_db, _userManager, cancel).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database initialization error");
            throw;
        }
    }
}
