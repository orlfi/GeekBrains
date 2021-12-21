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
    public partial class MainWindowViewModel : ViewModel
    {
        public string Title { get; set; } = "Привет из VSCode";

        public string _name = "";
        public string Name { get => _name; set => Set(ref _name, value); }

        // public ObservableCollection<FileSystemInfo> Files { get; set; }

        // private FileSystemInfo _selectedFile;
        // public FileSystemInfo SelectedFile
        // {
        //     get => _selectedFile;
        //     set
        //     {
        //         _selectedFile = value;
        //         OnPropertyChange("SelectedFile", ref value);
        //     }

        // }

        private ModelEventBus _messageBus;
        private FileSystemInfo _selectedFile;

        public MainWindowViewModel()
        {
            _messageBus = ModelEventBus.Instance;
            _messageBus.Subscribe<FileSelectionChangeEvent>((ev) => _selectedFile = (ev as FileSelectionChangeEvent).Selected);
            // Files = new ObservableCollection<FileSystemInfo>(GetFileSystem(@"c:\windows"));
        }

        public void OnPropertyChange<T>(string propertyName, ref T value)
        {
            MessageBox.Show(value.ToString());
        }

        // private ICollection<FileSystemInfo> GetFileSystem(string path)
        // {
        //     var info = new DirectoryInfo(path);
        //     return info.GetFileSystemInfos();
        // }
    }
}