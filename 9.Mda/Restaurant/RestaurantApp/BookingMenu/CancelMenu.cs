using Interfaces;
using Restaurant.Booking.BookingMenu.Base;

namespace Restaurant.Booking.BookingMenu;

public class CancelMenu : Menu, IMenu
{
    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.WriteLine("<Синхронная отмена бронирования>");
        Console.Write("Введите номер столика для отмены брони: ");
        var tableNumber = GetNumberAnswer("Введите номер:");

        manager.Restaurant.RemoveBookingByNumber(tableNumber);

        return false;
    }
}
