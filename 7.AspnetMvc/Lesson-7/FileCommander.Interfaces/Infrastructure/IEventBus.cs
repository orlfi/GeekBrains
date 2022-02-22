namespace FileCommander.Interfaces.Infrastructure;

public interface IEventBus
{
    public void Publish(IIntegrationEvent @event);
    public void Subscribe<TEvent>(Action<IIntegrationEvent> handler)
        where TEvent : IIntegrationEvent;
}
