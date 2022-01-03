using MailService.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace MailService.Services.Mail;

public class MailGatewayBuilder : IMailGatewayBuilder
{
    private MailGatewayOptions _options;
    public MailGatewayBuilder(IConfiguration configuration)
    {
        _options = new MailGatewayOptions();
        configuration.Bind("MailGatewayOptions", _options);
    }

    public MailGatewayBuilder WithOptions(MailGatewayOptions options)
    {
        _options = options;
        return this;
    }

    public async Task<MailGateway> Async(CancellationToken cancel = default)
    {
        if (_options is null)
            throw new NullReferenceException("Настройки почтового сервера не заполнены");

        var gateway = new MailGateway(_options);
        await gateway.InitializeAsync(cancel).ConfigureAwait(false);
        return gateway;
    }
}
