using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace FileCommander.Infrastructure.EventBus.Interfaces
{
    public interface IEventBus
    {
        public void Publish(IIntegrationEvent @event);
        public void Subscribe<TEvent>(Action<IIntegrationEvent> handler)
            where TEvent : IIntegrationEvent;
    }
}