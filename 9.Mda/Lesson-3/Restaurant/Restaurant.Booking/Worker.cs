using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.DTO;
using Restaurant.Booking.Interfaces;
using Restaurant.Messaging.Data;

namespace Restaurant.Booking;

internal class Worker : BackgroundService
{
    private readonly ILogger _logger;
    private readonly ITableBookingService _restaurant;
    private readonly IBus _bus;

    public Worker(ILogger<Worker> logger, ITableBookingService restaurant, IBus bus)
    {
        _logger = logger;
        _restaurant = restaurant;
        _bus = bus;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (true)
            {
                Task.Run(async () =>
                    {
                        var seats = Random.Shared.Next(1, 10);
                        _logger.LogInformation("Автоматическое асинхронное резервирование столика на {Seats} мест.", seats);
                        var orderId = NewId.NextGuid();
                        var clientId = Guid.NewGuid();
                        var dish = MenuRepository.GetDishById(Random.Shared.Next(1, MenuRepository.Count + 1));

                        await _bus.Publish(new BookingRequested() { OrderId = orderId, ClientId = clientId, Dish = dish, Seats = seats});
                    },
                    stoppingToken
                );
                Console.ReadLine();
                // Thread.Sleep(5000);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {Message}", ex.Message);
        }

        return Task.CompletedTask;
    }
}