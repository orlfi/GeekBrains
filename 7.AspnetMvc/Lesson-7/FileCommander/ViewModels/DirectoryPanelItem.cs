using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FileCommander.ViewModels
{
    public class DirectoryPanelItem : FilePanelItemBase
    {
        private readonly DirectoryInfo _info;

        public override string Name => _info.Name;

        public override string Created => _info.CreationTime.ToString(createdTemplate);

        public DirectoryPanelItem(DirectoryInfo info)
        {
            _info = info;
            Icon = ToImageSource(info.FullName);
        }
    }
}