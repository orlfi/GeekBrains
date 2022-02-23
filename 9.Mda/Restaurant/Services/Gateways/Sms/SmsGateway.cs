using Services.Interfaces;

namespace Services.Gateways.Sms;

public class SmsGateway : IGateway
{
    public string GatewayName => "СМС сообщение";

    public async Task SendAsync(string message, CancellationToken cancel = default)
    {
        await Task.Delay(1000);
        Console.WriteLine(message);
    }
}
