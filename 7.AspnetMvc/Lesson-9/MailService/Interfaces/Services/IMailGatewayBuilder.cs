using MailService.Services.Mail;

namespace MailService.Interfaces.Services;

public interface IMailGatewayBuilder
{
    Task<MailGateway> Async(CancellationToken cancel = default);
    MailGatewayBuilder WithOptions(MailGatewayOptions options);
}
