using Restaurant.Messaging.Interfaces;

namespace Restaurant.Messaging.Sms;

public class SmsProducer : IProducer
{
    public string Name => "СМС";

    public void Send(string message)
    {
        Console.WriteLine(message);
    }
 }
