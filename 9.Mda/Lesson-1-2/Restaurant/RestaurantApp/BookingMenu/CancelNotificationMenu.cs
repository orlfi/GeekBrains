using Interfaces;
using Restaurant.Booking.BookingMenu.Base;

namespace Restaurant.Booking.BookingMenu;

public class CancelNotificationMenu : Menu, IMenu
{
    private readonly IEnumerable<string> _choices;

    public CancelNotificationMenu(string gatewayName)
    {
        _choices = new List<string>()
        {
            $"1 - мы уведомим Вас по {gatewayName} (асинхронно)",
            "2 - подождите на линии, мы Вас оповестим (синхронно )",
        };
    }

    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.WriteLine("<Уведомление об отмене бронирования>");
        foreach (var choice in _choices)
        {
            Console.WriteLine(choice);
        }
        var notificationChoice = GetNumberAnswer((value) => value is (1 or 2), "Введите 1 или 2");
        
        if (notificationChoice == 1)
            manager.CurrentMenu = new CancelAsyncMenu();
        else
            manager.CurrentMenu = new CancelMenu();

        return true;
    }
}
