using MailService.Data;
using MailService.Interfaces.Services;

namespace MailService.Services.Sms;

public class SmsGateway : IGateway
{
    public Task SendAsync(Message message, CancellationToken cancel = default)
    {
        throw new NotImplementedException();
    }
}
