using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileCommander.Infrastructure.EventBus.Events.Base;

namespace FileCommander.Infrastructure.EventBus.Events
{
    public class FileSelectionChangeEvent : IntegrationEvent
    {
        public FileSystemInfo Selected { get; init; }

        public string FilePanelName { get; init; }

        public FileSelectionChangeEvent(FileSystemInfo info, string filePanelName)
        {
            Selected = info;
            FilePanelName = filePanelName;
        }
    }
}