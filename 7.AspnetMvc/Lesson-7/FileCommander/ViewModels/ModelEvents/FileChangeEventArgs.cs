using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileCommander.ViewModels.Interfaces;

namespace FileCommander.ViewModels.ModelEvents;

public class FileChangeEventArgs : EventArgs
{
    public string Path { get; init; }

    public FileChangeEventArgs(string path) => Path = path;
}
