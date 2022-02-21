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
using FileCommander.ViewModels.Base;

namespace FileCommander.ViewModels.FileItems
{
    public class FilePanelItem : FilePanelItemBase
    {
        private readonly FileInfo _info;

        public override string Name => _info.Name;

        public override string Created => _info.CreationTime.ToString(createdTemplate);

        public override string FullName => _info.FullName;

        public override string Attributes => string.Format("{0}{1}{2}{3}",
            _info.Attributes.HasFlag(FileAttributes.ReadOnly) ? "R" : "",
            _info.Attributes.HasFlag(FileAttributes.Hidden) ? "H" : "",
            _info.Attributes.HasFlag(FileAttributes.System) ? "S" : "",
             _info.Attributes.HasFlag(FileAttributes.Archive) ? "A" : "");

        public FilePanelItem(FileInfo fileInfo)
        {
            _info = fileInfo;
            Icon = ToImageSource(fileInfo);
            _size = fileInfo.Length;
        }

        protected override string FormatSize(long size)
        {
            return _info.Length.ToString("N0");
        }
    }
}