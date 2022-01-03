using MailService.Services.Mail;

namespace MailService.Interfaces.Services;

public interface IMailGatewayBuilder
{
    Task<MailGateway> Build(CancellationToken cancel = default);
    MailGatewayBuilder WithOptions(MailGatewayOptions options);
}
