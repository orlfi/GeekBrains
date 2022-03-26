using Interfaces;
using Services.BookingMenu.Base;
namespace Services.ChoiceStates;

public class CancelAsyncMenu : Menu, IMenu
{
    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.Write("<Асинхронная отмена бронирования>");
        Console.Write("Введите номер столика для отмены брони: ");
        var tableNumber = GetNumberAnswer("Введите номер:");

        manager.Restaurant.RemoveBookingByNumberAsync(tableNumber);

        return false;
    }
}
