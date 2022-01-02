using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace MailService.Services;

public class MailGateway : IDisposable
{
    private readonly SmtpClient _client = new SmtpClient();
    private readonly MailGatewayOptions _options;

    public MailGateway(MailGatewayOptions options)
    {
        if (options is null)
            throw new ArgumentNullException("options");

        _options = options;
        Initialize();
    }
    private void Initialize()
    {
        _client.Connect(_options.SmtpServer, _options.Port);
        _client.AuthenticateAsync(_options.UserName, _options.Password);
    }

    public async Task InitializeAsync(CancellationToken cancel = default)
    {
        await _client.ConnectAsync(
            _options.SmtpServer,
            _options.Port,
            MailKit.Security.SecureSocketOptions.Auto,
            cancel).ConfigureAwait(false);

        await _client.AuthenticateAsync(_options.UserName, _options.Password, cancel).ConfigureAwait(false);
    }

    public void Dispose()
    {
        _client.Disconnect(true);
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}
