using MassTransit;
using Microsoft.Extensions.Logging;
using Restaurant.Booking.DTO;
using Restaurant.Booking.Interfaces;
using Restaurant.Messaging.Interfaces;

namespace Restaurant.Notifications.Consumers;

internal class BookingRequestedConsumer : IConsumer<IBookingRequested>
{
    private readonly ILogger<BookingRequestedConsumer> _logger;
    private readonly ITableBookingService _tableBookingService;

    public BookingRequestedConsumer(ILogger<BookingRequestedConsumer> logger, ITableBookingService tableBookingService)
    {
        _logger = logger;
        _tableBookingService = tableBookingService;
    }

    public async Task Consume(ConsumeContext<IBookingRequested> context)
    {
        var request = context.Message;

        _logger.LogInformation("Получено сообщение на бронирование столиков на {Seats} мест] для заказа OrderId = {OrderId}", request.Seats, request.OrderId);

        var result = await _tableBookingService.BookFreeTableAsync(request.Seats);
        if (result.Success)
        {
            _logger.LogInformation("Забронированы столики {TableNumbers} для клиента {ClientId}", string.Join(",", result.TableNumbers), request.ClientId.ToString());
            await _tableBookingService.AddOrder(request.OrderId, result.TableNumbers, request.Dish);
            await context.Publish(new TableBooked() { OrderId = request.OrderId, ClientId = request.ClientId, Dish = request.Dish});
        }
        else
        {
            _logger.LogWarning("Ошибка бронирования столиков для клиента {ClientId}: {Error}", request.ClientId.ToString(), result.Error);
            await context.Publish(new BookingCancel() { OrderId = request.OrderId});
        }
    }
}
