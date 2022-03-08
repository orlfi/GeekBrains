using Interfaces;
using Restaurant.Booking.BookingMenu;

namespace Restaurant.Booking.Services;

public class AutomaticOrderManager : IOrderManager
{
    private readonly IRestaurant _restaurant;

    public IRestaurant Restaurant => _restaurant;

    public AutomaticOrderManager(IRestaurant restaurant)
    {
        _restaurant = restaurant;
    }

    public void ProcessRequest()
    {
        var seats = Random.Shared.Next(1, 10);
        Console.WriteLine($"Автоматическое асинхронное резервирование столика на {seats} мест.");
        _restaurant.BookFreeTableAsync(seats);
        Thread.Sleep(3000);
    }
}
