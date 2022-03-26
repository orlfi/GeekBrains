using Interfaces;
using Services.BookingMenu.Base;

namespace Services.ChoiceStates;

public class BookingNotificationMenu : Menu, IMenu
{
    private readonly IEnumerable<string> _choices;

    public BookingNotificationMenu(string gatewayName)
    {
        _choices = new List<string>()
        {
            $"1 - мы уведомим Вас по {gatewayName} (асинхронно)",
            "2 - подождите на линии, мы Вас оповестим (синхронно )",
        };
    }

    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.WriteLine("<Уведомление о бронировании>");

        foreach (var choice in _choices)
        {
            Console.WriteLine(choice);
        }
        var notificationChoice = GetNumberAnswer((value) => value is (1 or 2), "Введите 1 или 2");

        if (notificationChoice == 1)
            manager.CurrentMenu = new BookingAsyncMenu();
        else
            manager.CurrentMenu = new BookingMenu();

        return true;
    }
}
