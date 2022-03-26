using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Booking.BookingMenu;

namespace Restaurant.Booking.Services;

public class InteractiveOrderManager : IInteractiveOrderManager
{
    private readonly IRestaurantBooking _restaurant;
    private readonly IServiceProvider _provider;

    public IRestaurantBooking Restaurant => _restaurant;
    public IMenu? CurrentMenu { get; set; }

    public InteractiveOrderManager(IRestaurantBooking restaurant, IServiceProvider provider)
    {
        _restaurant = restaurant;
        _provider = provider;
    }

    public void ProcessRequest()
    {
        CurrentMenu = _provider.GetRequiredService<MainMenu>();
        while (CurrentMenu.GetChoice(this)) ;
    }
}
