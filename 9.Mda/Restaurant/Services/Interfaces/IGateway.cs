namespace Services.Interfaces;

public interface IGateway
{
    string GatewayName { get;  }
    Task SendAsync(string message, CancellationToken cancel = default);
}
