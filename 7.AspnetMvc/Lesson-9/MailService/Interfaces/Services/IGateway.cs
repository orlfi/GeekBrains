using MailService.Data;

namespace MailService.Interfaces.Services;

public interface IGateway
{
    Task SendAsync(Message message, CancellationToken cancel = default);
}
