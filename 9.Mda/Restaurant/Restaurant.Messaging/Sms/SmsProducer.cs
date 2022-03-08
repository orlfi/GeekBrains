using Restaurant.Messaging.Interfaces;

namespace Restaurant.Messaging.Sms;

public class SmsProducer : IProducer, IDisposable
{
    bool _disposed;
    public string Name => "СМС";

    public void Send(string message)
    {
        Console.WriteLine(message);
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
    }

}
