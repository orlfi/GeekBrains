using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FileCommander.Commands.Base;
using FileCommander.Infrastructure.EventBus;
using FileCommander.Infrastructure.EventBus.Events;
using FileCommander.ViewModels.Base;
using FileCommander.ViewModels.Interfaces;

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

        public ObservableCollection<IFilePanelItem> Files { get; set; }

        private IFilePanelItem _selectedFile;

        public IFilePanelItem SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
                _messageBus.Publish(new FileSelectionChangeEvent(_selectedFile, this.GetType().Name));
                // OnPropertyChange("SelectedFile", ref value);
            }
        }

        public void SelectItem(string sourceName)
        {
            MessageBox.Show(sourceName);
        }

        private ModelEventBus _messageBus;

        public FilePanelViewModel()
        {
            Files = new(GetFiles(@"c:\windows"));
            _messageBus = ModelEventBus.Instance;
        }

        // public void OnPropertyChange<T>(string propertyName, ref T value)
        // {
        // }

        private ICollection<IFilePanelItem> GetFiles(string path)
        {
            var di = new DirectoryInfo(path);
            var directories = di.GetDirectories().Select(item => new DirectoryPanelItem(item) as IFilePanelItem);
            var files = di.GetFiles().Select(item => new FilePanelItem(item));
            var result = directories.Union(files);
            return result.ToArray();
        }
    }
}