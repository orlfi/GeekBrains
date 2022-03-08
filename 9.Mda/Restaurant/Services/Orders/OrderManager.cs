using System.Reflection.Metadata.Ecma335;
using Services.Interfaces;

namespace Services.Request;

public class OrderManager : IOrderManager
{
    private readonly INotificationGateway _notificationService;
    private readonly IOrderResultResolver _resultResolver;

    public OrderManager(INotificationGateway notificationService, IOrderResultResolver resultResolver)
    {
        _notificationService = notificationService;
        _resultResolver = resultResolver;
    }

    public void ProcessRequest()
    {
        Console.WriteLine("Добрый день.");
        Console.WriteLine("1 - Желаете забронировать столик?");
        Console.WriteLine("2 - Желаете отменить бронь?");
        var bookingChoice = GetNumberAnswer((value) => value is (1 or 2), "Введите 1 или 2");

        HandleBookingChoice(bookingChoice);
    }

    private void HandleBookingChoice(int bookingChoice)
    {
        Console.WriteLine($"1 - мы уведомим Вас по {_notificationService.GatewayName} (асинхронно)");
        Console.WriteLine($"2 - подождите на линии, мы Вас оповестим (синхронно )");
        var notificationChoice = GetNumberAnswer((value) => value is (1 or 2), "Введите 1 или 2");

        if (bookingChoice == 1)
            HandleSetBookingNotificationChoice(notificationChoice);
        else
            HandleClearBookingNotificationChoice(notificationChoice);
    }

    private void HandleSetBookingNotificationChoice(int bookingChoice)
    {
        Console.Write("Введите количество мест: ");
        var seatsCount = GetNumberAnswer("Введите номер:");

        if (bookingChoice == 1)
            _resultResolver.Handle(new SetBookingAsyncResult(seatsCount));
        else
            _resultResolver.Handle(new SetBookingResult(seatsCount));
    }

    private void HandleClearBookingNotificationChoice(int bookingChoice)
    {
        Console.Write("Введите номер столика для отмены брони: ");
        var tableNumber = GetNumberAnswer("Введите номер:");

        if (bookingChoice == 1)
            _resultResolver.Handle(new ClearBookingAsyncResult(tableNumber));
        else
            _resultResolver.Handle(new ClearBookingResult(tableNumber));
    }

    private int GetNumberAnswer(string wrongConditionMessage)
    {
        return GetNumberAnswer(_ => true, wrongConditionMessage);
    }

    private int GetNumberAnswer(Func<int, bool> condition, string wrongConditionMessage)
    {
        var answer = Console.ReadLine();
        if (!(int.TryParse(answer, out int value) && condition(value)))
        {
            Console.WriteLine(wrongConditionMessage);
            return GetNumberAnswer(condition, wrongConditionMessage);
        }
        return value;
    }
}
