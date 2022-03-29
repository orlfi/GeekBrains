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

                        var result = await _restaurant.BookFreeTableAsync(seats);

                        if (result.Success)
                        {
                            _logger.LogInformation("Забронированы столики {TableNumbers} для клиента {ClientId}", string.Join(",", result.TableNumbers), clientId.ToString());
                            await _restaurant.AddOrder(orderId, result.TableNumbers, dish);
                        }
                        else
                        {
                            _logger.LogWarning("Ошибка бронирования столиков для клиента {ClientId}: {Error}", clientId.ToString(), result.Error);
                        }

                        _logger.LogInformation("Отправка сообщения TableBooked: OrderId = {OrderId}, ClientId = {ClientId} Success = {Success} ", orderId, clientId, result.Success);
                        await _bus.Publish(new TableBooked() { OrderId = orderId, ClientId = clientId, Dish = dish, Success = result.Success });
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