using ReportSender.Domain;
using ReportSender.Interfaces.Gateways;

namespace ReportSender.Services.Gateways.Telegram;

public class TelegramGateway : IGateway
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task SendAsync(Message message, CancellationToken cancel = default)
    {
        throw new NotImplementedException();
    }
}
