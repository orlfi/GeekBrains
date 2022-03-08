using Interfaces;

namespace Restaurant.Messaging.Sms;

public class SmsProducer : IProducer
{
    public string Name => "СМС";

    public async Task SendAsync(string message, CancellationToken cancel = default)
    {
        await Task.Delay(1000);
        Console.WriteLine(message);
    }
}
