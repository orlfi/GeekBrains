using Interfaces;

namespace Services.Gateways.Sms;

public class SmsGateway : INotificationGateway
{
    public string GatewayName => "СМС";

    public async Task SendAsync(string message, CancellationToken cancel = default)
    {
        await Task.Delay(1000);
        Console.WriteLine(message);
    }
}
