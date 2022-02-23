using Microsoft.Extensions.Logging;
using Models;
using Services.Enums;
using Services.Interfaces;
using System.Collections.Concurrent;
using System.Data;

namespace Services;

public class Restaurant : IDisposable, IRestaurant
{
    private const int ClearBookingTimerPeriod = 20000;
    private const int SearchFreeTableTime = 20000;
    private const int ClearBookingTime = 100;
    private const int ClearAllBookingTime = 1000;

    private readonly IGateway _asyncNotificationService;
    private readonly ILogger<Restaurant> _logger;
    private List<Table> _tables { get; set; } = new();
    private System.Timers.Timer _timer;
    private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);


    public Restaurant(IGateway asyncNotificationService, ILogger<Restaurant> logger)
    {
        _asyncNotificationService = asyncNotificationService;
        _logger = logger;
        _timer = new System.Timers.Timer(ClearBookingTimerPeriod);
        _timer.Elapsed += async (s, e) => await ClearBooksAsync();
    }

    public void BookFreeTable()
    {
        Console.WriteLine($"Добрый день. Подождите секунду, я подберу столик и подтвержу бронь. Оставайтесь на линии");
        string message = "";
        try
        {
            var table = _tables.FirstOrDefault(item => item.State != State.Free);
            Thread.Sleep(SearchFreeTableTime);
            table?.SetBooking(State.Booked);
            message = table is null
                ? "Столиков нет"
                : $"Забронирован столик {table.Id}";
        }
        catch
        {
            message = $"Сервис временно недоступен. Приносим свои извенения.";
        }

        Console.WriteLine(message);
    }

    public async Task BookFreeTableAsync(CancellationToken cancel = default)
    {
        Console.WriteLine($"Добрый день. Подождите секунду, я подберу столик и подтвержу бронь. Вам придет {_asyncNotificationService.GatewayName}");
        await semaphoreSlim.WaitAsync(cancel).ConfigureAwait(false);
        string message = "";
        try
        {
            var table = _tables.FirstOrDefault(item => item.State != State.Free);
            await Task.Delay(SearchFreeTableTime, cancel).ConfigureAwait(false);
            table?.SetBooking(State.Booked);
            message = table is null
                ? "Столиков нет"
                : $"Забронирован столик {table.Id}";
        }
        catch
        {
            message = $"Сервис временно недоступен. Приносим свои извенения. Вас уведомим {_asyncNotificationService.GatewayName} о решении проблемы";
        }
        finally
        {

            semaphoreSlim.Release();
        }

        await _asyncNotificationService.SendAsync(message);
    }

    public void ClearBook(int number)
    {
        Console.WriteLine($"Добрый день. Подождите секунду, я сниму бронь со столика {number}. Оставайтесь на линии");

        string message = "";
        var table = _tables.SingleOrDefault(item => item.Id == number);

        table?.SetBooking(State.Free);
        message = table is null
            ? "Столик не найден нет"
            : $"Бронь снята со столика {table.Id}";
        Console.WriteLine(message);
    }

    public async Task ClearBookAsync(int number, CancellationToken cancel = default)
    {
        Console.WriteLine($"Добрый день. Подождите секунду, я сниму бронь со столика {number}. Вам придет {_asyncNotificationService.GatewayName}");

        await semaphoreSlim.WaitAsync(cancel).ConfigureAwait(false);
        string message = "";
        try
        {
            await Task.Delay(ClearBookingTime, cancel).ConfigureAwait(false);
            var table = _tables.SingleOrDefault(item => item.Id == number);

            table?.SetBooking(State.Free);
            message = table is null
                ? "Столик не найден нет"
                : $"Бронь снята со столика {table.Id}";
        }
        finally
        {
            semaphoreSlim.Release();
        }
        await _asyncNotificationService.SendAsync(message);
    }

    private async Task ClearBooksAsync(CancellationToken cancel = default)
    {
        await semaphoreSlim.WaitAsync(cancel).ConfigureAwait(false);
        try
        {
            await Task.Delay(ClearAllBookingTime, cancel).ConfigureAwait(false);
            foreach (var item in _tables.Where(item => item.State == State.Booked))
            {
                item.SetBooking(State.Free);
            }
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public void Dispose()
    {
        _timer.Dispose();
        semaphoreSlim.Dispose();
    }
}
