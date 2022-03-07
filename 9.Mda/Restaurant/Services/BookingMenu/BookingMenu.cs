using Services.BookingMenu.Base;
using Interfaces;

namespace Services.ChoiceStates;

public class BookingMenu : Menu, IMenu
{
    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.Write("<Синхронное бронирование столика>");
        Console.Write("Введите количество мест: ");
        var seatsCount = GetNumberAnswer("Введите номер:");

        manager.Restaurant.BookFreeTable(seatsCount);

        return false;
    }
}
