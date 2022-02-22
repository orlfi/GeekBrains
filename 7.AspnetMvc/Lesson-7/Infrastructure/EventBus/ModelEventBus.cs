using System.Collections.Concurrent;
using FileCommander.Interfaces.Infrastructure;

namespace FileCommander.Infrastructure.EventBus;

public class ModelEventBus : IEventBus
{
    private static ModelEventBus _instance;
    public static ModelEventBus Instance => _instance ??= new ModelEventBus();

    private ConcurrentDictionary<Type, Action<IIntegrationEvent>> _subscriptions = new();

    public void Subscribe<TEvent>(Action<IIntegrationEvent> handler) where TEvent : IIntegrationEvent
    {

        if (!_subscriptions.ContainsKey(typeof(TEvent)))
            _subscriptions.TryAdd(typeof(TEvent), handler);
        else
            _subscriptions[typeof(TEvent)] += handler;
    }

    public void Publish(IIntegrationEvent @event)
    {
        if (_subscriptions.ContainsKey(@event.GetType()))
            _subscriptions[@event.GetType()].Invoke(@event);
    }

}
