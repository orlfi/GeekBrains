using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FileCommander.ViewModels.Interfaces;

namespace FileCommander.ViewModels.ModelEvents;
public class FileSelectionChangeEventArgs : EventArgs
{
    public IFilePanelItem SelectedFile { get; init; }

    public FileSelectionChangeEventArgs(IFilePanelItem selectedFile) => SelectedFile = selectedFile;
}
