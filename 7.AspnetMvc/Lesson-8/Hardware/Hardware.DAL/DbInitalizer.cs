using Hardwares.DAL.Context;
using Hardwares.Domain;
using Hardwares.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardwares.DAL;

/// <summary>
/// Иницализирует базу данных
/// </summary>
public class DbInitalizer : IDbInitalizer
{
    private readonly HardwaresDb _db;
    private readonly ILogger<DbInitalizer> _logger;

    public DbInitalizer(HardwaresDb db, ILogger<DbInitalizer> logger)
    {
        _db = db;
        _logger = logger;
    }

    /// <summary>
    /// Удаляет базуданных если она существует
    /// </summary>
    /// <param name="cancel">токен отмены операции</param>
    public async Task RemoveAsync(CancellationToken cancel = default)
    {
        cancel.ThrowIfCancellationRequested();
        _logger.LogInformation("Удаление базы данных...");

        try
        {
            var sw = Stopwatch.StartNew();
            await _db.Database.EnsureDeletedAsync(cancel).ConfigureAwait(false);
            sw.Stop();
            _logger.LogInformation("База данных удалена за {0} мс", sw.ElapsedMilliseconds);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Удаление базы данных было прервано");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении базы данных {0}", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Инициализирует базу данных
    /// </summary>
    /// <param name="remove">удаляет базу дынных при значении true</param>
    /// <param name="cancel">токен отмены операции</param>
    public async Task Initialize(bool remove, CancellationToken cancel = default)
    {
        cancel.ThrowIfCancellationRequested();
        _logger.LogInformation("Инициализация базы данных...");
        try
        {
            if (remove)
                await RemoveAsync(cancel);

            var sw = Stopwatch.StartNew();
            _logger.LogInformation("Выполняется миграция базы данных...");
            await _db.Database.MigrateAsync(cancel).ConfigureAwait(false);
            _logger.LogInformation("Миграция базы данных выполнена за {0} мс", sw.ElapsedMilliseconds);

            _logger.LogInformation("Выполняется заполнение базы данных исходными данными...");
            sw.Restart();
            await InitializeHardwaresData(cancel).ConfigureAwait(false);
            sw.Stop();
            _logger.LogInformation("База данных заполнена данными за {0} мс", sw.ElapsedMilliseconds);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Инициализация базы данных прервана");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при инициализации базы данных {0}", ex.Message);
            throw;
        }
    }

    private async Task InitializeHardwaresData(CancellationToken cancel = default)
    {
        if (await _db.Hardwares.AnyAsync(cancel).ConfigureAwait(false))
        {
            _logger.LogInformation("Заполнение базы данных не требуется");
            return;
        }

        // await нужен, т.к. BeginTransactionAsync возвращает DbTransaction с интерфейсом IAsyncDisposable !!!
        await using var transaction = await _db.Database.BeginTransactionAsync(cancel).ConfigureAwait(false);

        _logger.LogInformation("Заполнение таблицы {0}...", nameof(_db.Hardwares));
        var sw = Stopwatch.StartNew();
        var hardwares = Enumerable.Range(1, 10).Select(index => new Hardware
        {
            Name = $"Оборудование - {index}",
            Description = $"Конфигурация оборудования - {index}",
            Cost = Random.Shared.Next(10, 100) * 1000,
            InstallationDate = DateTime.Today.AddYears(-Random.Shared.Next(2, 7))
        });

        await _db.Hardwares.AddRangeAsync(hardwares, cancel).ConfigureAwait(false);
        await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        sw.Stop();
        _logger.LogInformation("Таблица {0} заполнена за {1} мс", nameof(_db.Hardwares), sw.ElapsedMilliseconds);
        await transaction.CommitAsync(cancel).ConfigureAwait(false);
    }
}
