namespace Restaurant.Messaging.Interfaces;

public interface IProducer
{
    string Name { get;  }
    void Send(string message);
}
