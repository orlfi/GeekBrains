using MailService.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MailService.Services.Mail;

public class MailGatewayBuilder : IMailGatewayBuilder
{
    private readonly ILogger<MailGatewayBuilder> _logger;
    private readonly ILogger<MailGateway> _mailGatewayLogger;
    private MailGatewayOptions _options;

    public MailGatewayBuilder(IConfiguration configuration, ILogger<MailGatewayBuilder> logger, ILogger<MailGateway> mailGatewayLogger)
    {
        _logger = logger;
        _mailGatewayLogger = mailGatewayLogger;
        _options = new MailGatewayOptions();
        configuration.Bind("MailGatewayOptions", _options);
    }

    public MailGatewayBuilder WithOptions(MailGatewayOptions options)
    {
        _options = options;
        return this;
    }

    public MailGateway Build()
    {
        if (_options is null)
            throw new NullReferenceException("Настройки почтового сервера не заполнены");

        var gateway = new MailGateway(_options, _mailGatewayLogger);
        gateway.Initialize();
        return gateway;
    }

    public async Task<MailGateway> BuildAsync(CancellationToken cancel = default)
    {
        if (_options is null)
            throw new NullReferenceException("Настройки почтового сервера не заполнены");

        var gateway = new MailGateway(_options, _mailGatewayLogger);
        await gateway.InitializeAsync(cancel).ConfigureAwait(false);
        return gateway;
    }
}
