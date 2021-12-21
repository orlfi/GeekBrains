using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileCommander.Infrastructure.EventBus.Interfaces;

namespace FileCommander.Infrastructure.EventBus.Events.Base
{
    public abstract class IntegrationEvent : IIntegrationEvent
    {
        public string Name { get; set; }
    }
}