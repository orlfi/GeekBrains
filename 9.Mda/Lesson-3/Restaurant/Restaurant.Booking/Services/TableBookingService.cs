using Microsoft.Extensions.Logging;
using Restaurant.Booking.Enums;
using Restaurant.Booking.Interfaces;
using Restaurant.Booking.Models;
using System.Data;

namespace Restaurant.Booking.Services;

internal class TableBookingService : IDisposable, ITableBookingService
{
    private const int ClearBookingTimerPeriod = 20000;
    private const int SearchFreeTableTime = 3000;
    private const int ClearBookingTime = 1000;
    private const int ClearAllBookingTime = 300;
    private const int TablesCount = 5;
    private const int MinSeatsTable = 3;
    private const int MaxSeatsTable = 5;

    private readonly ILogger<TableBookingService> _logger;
    private List<Table> _tables { get; set; } = new(TablesCount);
    private System.Timers.Timer _timer;
    private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);


    public TableBookingService(ILogger<TableBookingService> logger)
    {
        _logger = logger;
        InitTables();

        _timer = new System.Timers.Timer(ClearBookingTimerPeriod);
        _timer.Elapsed += async (s, e) => await RemoveBookingsAsync();
        _timer.Start();
    }

    private void InitTables()
    {
        for (int i = 0; i < TablesCount; i++)
        {
            _tables.Add(new Table(Random.Shared.Next(MinSeatsTable, MaxSeatsTable + 1)));
        }
    }

    public async Task<BookinResult> BookFreeTableAsync(int seatsCount, CancellationToken cancel = default)
    {
        _logger.LogInformation("Добрый день. Подождите секунду, я подберу столик и подтвержу бронь. ");

        await semaphoreSlim.WaitAsync(cancel).ConfigureAwait(false);
        try
        {
            var tables = GetFreeTables(seatsCount);
            await Task.Delay(SearchFreeTableTime, cancel).ConfigureAwait(false);
            if (tables.Count > 0)
            {
                foreach (var item in tables)
                {
                    item.SetBooking(State.Booked);
                }
                //message = $"[{DateTime.Now.ToString("hh:mm:ss")}] УВЕДОМЛЕНИЕ. Ваш столик №{string.Join(",", tables.OrderBy(item => item.Id).Select(item => item.Id))}";
                var result = tables.OrderBy(item => item.Id).Select(item => item.Id).ToList();
                return result;
            }
            else
            {
                return $"[{DateTime.Now.ToString("hh:mm:ss")}] Столиков нет";

            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при бронировании столика на {0}", seatsCount);
            return $"Сервис временно недоступен. Приносим свои извенения. Вас уведомим  о решении проблемы";
        }
        finally
        {

            semaphoreSlim.Release();
        }
    }

    private List<Table> GetFreeTables(int seats)
    {
        Table? table;
        int seatsLeft = seats;
        var result = new List<Table>();

        while (seatsLeft > 0)
        {
            var freetables = _tables.Where(item => item.State == State.Free && !result.Contains(item)).ToArray();

            if (freetables.Length == 0)
                return new List<Table>();

            var maxSeatsLeft = freetables.Max(item => item.Seats);
            if (seatsLeft >= maxSeatsLeft)
                table = freetables?.OrderByDescending(item => item.Seats).FirstOrDefault();
            else
                table = freetables?.OrderBy(item => item.Seats).FirstOrDefault();

            if (table == null)
                return new List<Table>();

            result.Add(table);
            seatsLeft -= table.Seats;
        }

        return result;
    }

    public bool RemoveBookingByNumberAsync(int number, CancellationToken cancel = default)
    {
        _logger.LogInformation("Добрый день. Подождите секунду, я сниму бронь со столика {Nnumber}.", number);
        Task.Run(async () =>
        {
            await semaphoreSlim.WaitAsync(cancel).ConfigureAwait(false);
            string message = "";
            try
            {
                await Task.Delay(ClearBookingTime, cancel).ConfigureAwait(false);
                var table = _tables.SingleOrDefault(item => item.Id == number && item.State == State.Booked);

                table?.SetBooking(State.Free);
                message = table is null
                    ? "Столик не найден или не забронирован"
                    : $"Бронь снята со столика {table.Id}";
            }
            finally
            {
                semaphoreSlim.Release();
            }
            //_notificationService.Send(message);
        });
        return true;
    }

    private async Task RemoveBookingsAsync(CancellationToken cancel = default)
    {
        _logger.LogInformation("Снятие брони со всех столов...");
        var bookedTablesNumbers = new List<int>();
        await semaphoreSlim.WaitAsync(cancel).ConfigureAwait(false);
        try
        {
            await Task.Delay(ClearAllBookingTime, cancel).ConfigureAwait(false);
            foreach (var item in _tables.Where(item => item.State == State.Booked))
            {
                item.SetBooking(State.Free);
                bookedTablesNumbers.Add(item.Id);
            }
        }
        finally
        {
            semaphoreSlim.Release();
        }
        _logger.LogInformation("Бронь снята со столов {0}...", string.Join(",", bookedTablesNumbers));
    }

    public void Dispose()
    {
        _timer.Dispose();
        semaphoreSlim.Dispose();
    }
}
