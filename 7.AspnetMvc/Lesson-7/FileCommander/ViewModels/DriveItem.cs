using FileCommander.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCommander.ViewModels;

public  class DriveItem : IDriveItem
{
    public string Name { get; set; } = null!;
    public string Path { get; init; } = null!;

    public override string ToString()
    {
        return Name;
    }
}
