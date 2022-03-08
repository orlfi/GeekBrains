using Interfaces;
using Restaurant.Booking.BookingMenu.Base;

namespace Restaurant.Booking.BookingMenu;

public class CancelAsyncMenu : Menu, IMenu
{
    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.WriteLine("<Асинхронная отмена бронирования>");
        Console.Write("Введите номер столика для отмены брони: ");
        var tableNumber = GetNumberAnswer("Введите номер:");

        manager.Restaurant.RemoveBookingByNumberAsync(tableNumber);

        return false;
    }
}
