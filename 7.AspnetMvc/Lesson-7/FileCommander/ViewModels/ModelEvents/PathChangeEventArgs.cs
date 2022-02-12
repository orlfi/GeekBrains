using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileCommander.ViewModels.Interfaces;

namespace FileCommander.ViewModels.ModelEvents;
public class PathChangeEventArgs : EventArgs
{
    public string Path { get; init; }

    public PathChangeEventArgs(string path) => Path = path;
}
