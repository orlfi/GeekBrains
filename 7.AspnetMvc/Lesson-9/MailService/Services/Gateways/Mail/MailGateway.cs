using MailKit.Net.Smtp;
using MailService.Data;
using MailService.Interfaces.Services;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace MailService.Services.Mail;

public class MailGateway : IDisposable, IGateway
{
    private readonly SmtpClient _client = new SmtpClient();

    private readonly ILogger<MailGateway> _logger;
    
    private readonly MailGatewayOptions _options;

    public MailGateway(MailGatewayOptions options, ILogger<MailGateway> logger)
    {

        if (options is null)
        {
            logger.LogError("Отсутствуют настройки почтового сервера");
            throw new ArgumentNullException("options");
        }
        
        _logger = logger;
        _options = options;
    }


    public async Task SendAsync(Message message, CancellationToken cancel = default)
    {
        MimeMessage mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_options.SenderName, _options.SenderEmail));
        mimeMessage.To.Add(new MailboxAddress(message.Name, message.To));
        mimeMessage.Subject = message.Subject;
        mimeMessage.Body = new TextPart(message.IsHtml ? TextFormat.Html : TextFormat.Text)
        {
            Text = message.Body
        };
        _logger.LogInformation("Отправка сообщения с темой {0} на адрес {1}...", message.Subject, message.To);
        await _client.SendAsync(mimeMessage, cancel).ConfigureAwait(false);
        _logger.LogInformation("Сообщение отправлено на адрес {0}", message.To);
    }

    public void Initialize()
    {
        if (!_client.IsConnected)
            _client.Connect(_options.SmtpServer, _options.Port);

        _client.AuthenticateAsync(_options.UserName, _options.Password);
    }

    public async Task InitializeAsync(CancellationToken cancel = default)
    {
        if (!_client.IsConnected)
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
