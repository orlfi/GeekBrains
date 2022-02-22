using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileCommander.ViewModels.Interfaces;

namespace FileCommander.Infrastructure.EventBus.Events
{
    public class FileSelectionChangeEvent : IntegrationEvent
    {
        public IFilePanelItem Selected { get; init; }

        public string FilePanelName { get; init; }

        public FileSelectionChangeEvent(IFilePanelItem info, string filePanelName)
        {
            Selected = info;
            FilePanelName = filePanelName;
        }
    }
}