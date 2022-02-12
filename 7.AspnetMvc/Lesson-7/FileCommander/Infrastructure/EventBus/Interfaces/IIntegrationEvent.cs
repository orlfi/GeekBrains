using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace FileCommander.Infrastructure.EventBus.Interfaces;

public interface IIntegrationEvent
{
    public string Name { get; set; }
}
