using Interfaces;
using Restaurant.Booking.BookingMenu;

namespace Restaurant.Booking.Services;

public class InteractiveOrderManager : IInteractiveOrderManager
{
    private readonly IRestaurant _restaurant;

    public IRestaurant Restaurant => _restaurant;
    public IMenu? CurrentMenu { get; set; }

    public InteractiveOrderManager(IRestaurant restaurant)
    {
        _restaurant = restaurant;
    }

    public void ProcessRequest()
    {
        CurrentMenu = new MainMenu();
        while (CurrentMenu.GetChoice(this)) ;
    }
}
