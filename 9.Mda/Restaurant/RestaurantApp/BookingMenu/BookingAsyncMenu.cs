using Interfaces;
using Restaurant.Booking.BookingMenu.Base;

namespace Restaurant.Booking.BookingMenu;

public class BookingAsyncMenu : Menu, IMenu
{
    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.WriteLine("<Асинхронное бронирование столика>");
        Console.Write("Введите количество мест: ");
        var seatsCount = GetNumberAnswer("Введите номер:");

        manager.Restaurant.BookFreeTableAsync(seatsCount);

        return false;
    }
}
