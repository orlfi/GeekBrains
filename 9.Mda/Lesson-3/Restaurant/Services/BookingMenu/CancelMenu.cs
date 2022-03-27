using Interfaces;
using Services.BookingMenu.Base;

namespace Services.ChoiceStates;

public class CancelMenu : Menu, IMenu
{
    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.Write("<Синхронная отмена бронирования>");
        Console.Write("Введите номер столика для отмены брони: ");
        var tableNumber = GetNumberAnswer("Введите номер:");

        manager.Restaurant.RemoveBookingByNumber(tableNumber);

        return false;
    }
}
