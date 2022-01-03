using MailKit.Net.Smtp;
using MailService.Data;
using MailService.Interfaces.Services;
using MimeKit;
using MimeKit.Text;

namespace MailService.Services.Mail;

public class MailGateway : IDisposable, IGateway
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

    public async Task SendAsync(Message message, CancellationToken cancel = default)
    {
        MimeMessage mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_options.SenderName, _options.SenderEmail));
        mimeMessage.To.Add(new MailboxAddress(message.Name, message.To));
        mimeMessage.Body = new TextPart(message.IsHtml ? TextFormat.Html : TextFormat.Text)
        {
            Text = message.Body
        };
        await _client.SendAsync(mimeMessage, cancel).ConfigureAwait(false);
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
