using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FileCommander.Commands.Base;
using FileCommander.Infrastructure.EventBus;
using FileCommander.Infrastructure.EventBus.Events;
using FileCommander.ViewModels.Base;

namespace FileCommander.ViewModels
{
    public partial class FilePanelViewModel : ViewModel
    {
        private string _path;
        public string Path
        {
            get => _path;
            set => Set(ref _path, value);
        }

        public ObservableCollection<FileSystemInfo> Files { get; set; }
        private FileSystemInfo _selectedFile;
        public FileSystemInfo SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
                _messageBus.Publish(new FileSelectionChangeEvent(_selectedFile, "Test"));
                OnPropertyChange("SelectedFile", ref value);
            }

        }

        private ModelEventBus _messageBus;

        public FilePanelViewModel()
        {
            Files = new ObservableCollection<FileSystemInfo>(GetFileSystem(@"c:\windows"));
            _messageBus = ModelEventBus.Instance;
        }

        public void OnPropertyChange<T>(string propertyName, ref T value)
        {

        }

        private ICollection<FileSystemInfo> GetFileSystem(string path)
        {
            var info = new DirectoryInfo(path);
            return info.GetFileSystemInfos();
        }
    }
}