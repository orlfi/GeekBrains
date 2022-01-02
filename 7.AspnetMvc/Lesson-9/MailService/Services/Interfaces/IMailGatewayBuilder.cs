namespace MailService.Services.Interfaces;

public interface IMailGatewayBuilder
{
    Task<MailGateway> Build(CancellationToken cancel = default);
    MailGatewayBuilder WithOptions(MailGatewayOptions options);
}
