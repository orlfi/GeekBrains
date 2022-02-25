namespace Services.Interfaces;

public interface INotificationGateway
{
    string GatewayName { get;  }
    Task SendAsync(string message, CancellationToken cancel = default);
}
