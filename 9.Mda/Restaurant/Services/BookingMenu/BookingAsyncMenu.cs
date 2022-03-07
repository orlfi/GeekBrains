using Interfaces;
using Services.BookingMenu.Base;

namespace Services.ChoiceStates;

public class BookingAsyncMenu : Menu, IMenu
{
    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.Write("<Асинхронное бронирование столика>");
        Console.Write("Введите количество мест: ");
        var seatsCount = GetNumberAnswer("Введите номер:");

        manager.Restaurant.BookFreeTableAsync(seatsCount);
        
        return false;
    }
}
