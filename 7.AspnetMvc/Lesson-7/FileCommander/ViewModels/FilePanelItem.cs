using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using FileCommander.Interop;

namespace FileCommander.ViewModels
{
    public class FilePanelItem : FilePanelItemBase
    {
        private readonly FileSystemInfo _info;

        public override string Name => _info.Name;

        public override string Created => _info.CreationTime.ToString(createdTemplate);

        public FilePanelItem(FileInfo fileInfo)
        {
            _info = fileInfo;
            Icon = ToImageSource(fileInfo);
            _size = fileInfo.Length;
        }
    }
}