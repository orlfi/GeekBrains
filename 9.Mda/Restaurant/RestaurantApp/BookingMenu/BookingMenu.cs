using Interfaces;
using Restaurant.Booking.BookingMenu.Base;

namespace Restaurant.Booking.BookingMenu;

public class BookingMenu : Menu, IMenu
{
    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.WriteLine("<Синхронное бронирование столика>");
        Console.Write("Введите количество мест: ");
        var seatsCount = GetNumberAnswer("Введите номер:");

        manager.Restaurant.BookFreeTable(seatsCount);

        return false;
    }
}
