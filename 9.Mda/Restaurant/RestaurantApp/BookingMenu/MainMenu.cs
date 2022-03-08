using Interfaces;
using Restaurant.Booking.BookingMenu.Base;

namespace Restaurant.Booking.BookingMenu;

public class MainMenu : Menu, IMenu
{
    private readonly IEnumerable<string> _choices = new List<string>()
    {
        "1 - Желаете забронировать столик?",
        "2 - Желаете отменить бронь?",
    };

    public bool GetChoice(IInteractiveOrderManager manager)
    {
        Console.WriteLine("Добрый день.");
        Console.WriteLine("<Главное меню>");
        foreach (var choice in _choices)
        {
            Console.WriteLine(choice);
        }
        var bookingChoice = GetNumberAnswer((value) => value is (1 or 2), "Введите 1 или 2");

        if (bookingChoice == 1)
            manager.CurrentMenu = new BookingNotificationMenu("по СМС");
        else
            manager.CurrentMenu = new CancelNotificationMenu("по СМС");

        return true;
    }
}
