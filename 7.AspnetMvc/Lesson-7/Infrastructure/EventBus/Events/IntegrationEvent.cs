using FileCommander.Interfaces.Infrastructure;

namespace FileCommander.Infrastructure.EventBus.Events;

public abstract class IntegrationEvent : IIntegrationEvent
{
    public string Name { get; set; }
}
