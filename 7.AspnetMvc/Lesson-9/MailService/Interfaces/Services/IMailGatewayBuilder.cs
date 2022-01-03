using MailService.Services.Mail;

namespace MailService.Interfaces.Services;

public interface IMailGatewayBuilder
{
    MailGateway Build();

    Task<MailGateway> BuildAsync(CancellationToken cancel = default);

    MailGatewayBuilder WithOptions(MailGatewayOptions options);
}
