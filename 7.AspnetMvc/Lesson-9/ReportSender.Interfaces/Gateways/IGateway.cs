using ReportSender.Domain;

namespace ReportSender.Interfaces.Gateways;

public interface IGateway: IDisposable
{
    Task SendAsync(Message message, CancellationToken cancel = default);
}
