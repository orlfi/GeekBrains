using Services.Interfaces;

namespace Services.Request;

public class OrderManager : IOrderManager
{
    private readonly IGateway _asyncNotificationService;
    private readonly IOrderResultResolver _resultResolver;

    public OrderManager(IGateway asyncNotificationService, IOrderResultResolver resultResolver)
    {
        _asyncNotificationService = asyncNotificationService;
        _resultResolver = resultResolver;
    }

    public void ProcessRequest()
    {
        Console.WriteLine("Добрый день.");
        Console.WriteLine($"1 - Желаете забронировать столик?");
        Console.WriteLine($"2 - Желаете отменить бронь?");
        int bookingChoice;
        do
        {
            var answer = Console.ReadLine();
            if (int.TryParse(answer, out bookingChoice) && bookingChoice is not (1 or 2))
            {
                Console.WriteLine("Введите 1 или 2");
            }
        } while (bookingChoice is not (1 or 2));

        HandleBookingChoice(bookingChoice);
    }

    private void HandleBookingChoice(int bookingChoice)
    {
        int notificationChoice;
        do
        {
            Console.WriteLine($"1 - мы уведомим Вас по {_asyncNotificationService.GatewayName} (асинхронно)");
            Console.WriteLine($"2 - подождите на линии, мы Вас оповестим (синхронно )");
            var answer = Console.ReadLine();
            if (int.TryParse(answer, out notificationChoice) && notificationChoice is not (1 or 2))
            {
                Console.WriteLine("Введите 1 или 2");
            }
        } while (notificationChoice is not (1 or 2));

        if (bookingChoice == 1)
            HandleSetBookingNotificationChoice(notificationChoice);
        else
            HandleClearBookingNotificationChoice(notificationChoice);
    }

    private void HandleSetBookingNotificationChoice(int bookingChoice)
    {
        int seatsCount;
        Console.Write("Введите количество мест: ");
        while (true)
        {
            var answer = Console.ReadLine();
            if (int.TryParse(answer, out seatsCount))
            {
                break;
            }
            Console.Write("Введите число:");
        }

        if (bookingChoice == 1)
            _resultResolver.Handle(new SetBookingAsyncResult(seatsCount));
        else
            _resultResolver.Handle(new SetBookingResult(seatsCount));
    }

    private void HandleClearBookingNotificationChoice(int bookingChoice)
    {
        int tableNumber;
        {
            Console.Write("Введите номер столика для отмены брони: ");
            while (true)
            {
                var answer = Console.ReadLine();
                if (int.TryParse(answer, out tableNumber))
                {
                    break;
                }
                Console.Write("Введите номер:");
            }
        }

        if (bookingChoice == 1)
            _resultResolver.Handle(new ClearBookingAsyncResult(tableNumber));
        else
            _resultResolver.Handle(new ClearBookingResult(tableNumber));
    }
}
