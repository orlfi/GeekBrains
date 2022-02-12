using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FileCommander.ViewModels.Interfaces;
public interface IFilePanelItem
{
    ImageSource? Icon { get; init; }
    string Name { get; }
    string FullName { get; }
    string Created { get; }
    string Size { get; }
}
