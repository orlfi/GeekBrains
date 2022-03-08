using Interfaces;

namespace Restaurant.Messaging.Sms;

public class SmsProducer : IProducer, IDisposable
{
    bool _disposed;
    public string Name => "СМС";

    public async Task SendAsync(string message, CancellationToken cancel = default)
    {
        await Task.Delay(1000);
        Console.WriteLine(message);
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
    }

}
