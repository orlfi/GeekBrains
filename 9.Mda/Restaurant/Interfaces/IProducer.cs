namespace Interfaces;

public interface IProducer
{
    string Name { get;  }
    Task SendAsync(string message, CancellationToken cancel = default);
}
