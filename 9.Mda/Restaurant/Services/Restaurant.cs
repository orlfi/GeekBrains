using Microsoft.Extensions.Logging;
using Models;
using Services.Enums;
using Services.Interfaces;
using System.Data;

namespace Services;

public class Restaurant : IDisposable, IRestaurant
{
    private const int ClearBookingTimerPeriod = 60000;
    private const int SearchFreeTableTime = 5000;
    private const int ClearBookingTime = 100;
    private const int ClearAllBookingTime = 1000;
    private const int TablesCount = 5;
    private const int MinSeatsTable = 3;
    private const int MaxSeatsTable = 5;

    private readonly INotificationGateway _asyncNotificationService;
    private readonly ILogger<Restaurant> _logger;
    private List<Table> _tables { get; set; } = new(TablesCount);
    private System.Timers.Timer _timer;
    private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);


    public Restaurant(INotificationGateway asyncNotificationService, ILogger<Restaurant> logger)
    {
        _asyncNotificationService = asyncNotificationService;
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

    public void BookFreeTable(int seatsCount)
    {
        Console.WriteLine($"Добрый день. Подождите секунду, я подберу столик и подтвержу бронь. Оставайтесь на линии");
        string message = "";
        semaphoreSlim.Wait();
        try
        {
            var tables = GetFreeTables(seatsCount);
            Thread.Sleep(SearchFreeTableTime);
            if (tables.Count > 0)
            {
                foreach (var item in tables)
                {
                    item.SetBooking(State.Booked);
                }
                message = $"УВЕДОМЛЕНИЕ. Ваш столик №{string.Join(",", tables.OrderBy(item => item.Id).Select(item => item.Id))}";
            }
            else
            {
                message = "Столиков нет";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при бронировании столика на {0}", seatsCount);
            message = $"Сервис временно недоступен. Приносим свои извенения.";
        }
        finally
        {
            semaphoreSlim.Release();
        }

        Console.WriteLine(message);
    }

    public void BookFreeTableAsync(int seatsCount, CancellationToken cancel = default)
    {
        Console.WriteLine($"Добрый день. Подождите секунду, я подберу столик и подтвержу бронь. Вам придет {_asyncNotificationService.GatewayName}");
        Task.Run(async () =>
        {
            await semaphoreSlim.WaitAsync(cancel).ConfigureAwait(false);
            string message = "";
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
                    message = $"УВЕДОМЛЕНИЕ. Ваш столик №{string.Join(",", tables.OrderBy(item => item.Id).Select(item => item.Id))}";
                }
                else
                {
                    message = "Столиков нет";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при бронировании столика на {0}", seatsCount);
                message = $"Сервис временно недоступен. Приносим свои извенения. Вас уведомим {_asyncNotificationService.GatewayName} о решении проблемы";
            }
            finally
            {

                semaphoreSlim.Release();
            }

            await _asyncNotificationService.SendAsync(message);
        });
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

    public void RemoveBookingByNumber(int number)
    {
        Console.WriteLine($"Добрый день. Подождите секунду, я сниму бронь со столика {number}. Оставайтесь на линии");

        string message = "";
        Table? table = null;
        semaphoreSlim.Wait();
        try
        {
            table = _tables.SingleOrDefault(item => item.Id == number && item.State == State.Booked);
            table?.SetBooking(State.Free);
        }
        finally
        {
            semaphoreSlim.Release();
        }
        message = table is null
            ? "Столик не найден или не забронирован"
            : $"Бронь снята со столика {table.Id}";
        Console.WriteLine(message);
    }

    public void RemoveBookingByNumberAsync(int number, CancellationToken cancel = default)
    {
        Console.WriteLine($"Добрый день. Подождите секунду, я сниму бронь со столика {number}. Вам придет {_asyncNotificationService.GatewayName}");
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
            await _asyncNotificationService.SendAsync(message);
        });
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
