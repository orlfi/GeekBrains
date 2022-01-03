using MailService.Data;
using MailService.Interfaces.Services;

namespace MailService.Services.Telegram;

public class TelegramGateway : IGateway
{
    public Task SendAsync(Message message, CancellationToken cancel = default)
    {
        throw new NotImplementedException();
    }
}
