using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Request;

public class OrderResultResolver : IOrderResultResolver
{
    private readonly ILogger<OrderResultResolver> _logger;
    private readonly IRestaurant _restaurant;

    public OrderResultResolver(ILogger<OrderResultResolver> logger, IRestaurant restaurant)
    {
        _logger = logger;
        _restaurant = restaurant;
    }

    public void Handle(SetBookingResult result)
    {
        _logger.LogDebug("Синхронное резервирование столика на {0} персоны", result.SeatsCount);
        _restaurant.BookFreeTable(result.SeatsCount);
    }

    public void Handle(SetBookingAsyncResult result)
    {
        _logger.LogDebug("Асинхронное резервирование столика на {0} персоны", result.SeatsCount);
        _restaurant.BookFreeTableAsync(result.SeatsCount);
    }
    public void Handle(ClearBookingResult result)
    {
        _logger.LogDebug("Синхронное снятие брони столика №{0}", result.TableNumber);
        _restaurant.RemoveBookingByNumber(result.TableNumber);
    }

    public void Handle(ClearBookingAsyncResult result)
    {
        _logger.LogDebug("Асинхронное снятие брони столика №{0}", result.TableNumber);
        _restaurant.RemoveBookingByNumberAsync(result.TableNumber);
    }
}
