using ReportSender.Domain;

namespace ReportSender.Interfaces.Gateways;

public interface IMailGatewayBuilder
{
    IGateway Build();

    Task<IGateway> BuildAsync(CancellationToken cancel = default);

    IMailGatewayBuilder WithOptions(MailGatewayOptions options);
}
