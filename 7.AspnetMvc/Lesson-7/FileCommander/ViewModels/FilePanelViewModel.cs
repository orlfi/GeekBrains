using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FileCommander.Infrastructure.EventBus;
using FileCommander.ViewModels.Base;
using FileCommander.ViewModels.FileItems;
using FileCommander.ViewModels.Interfaces;
using FileCommander.ViewModels.ModelEvents;

namespace FileCommander.ViewModels;
public partial class FilePanelViewModel : ViewModel
{
    public event EventHandler<FileSelectionChangeEventArgs> FileSelectionChangeEvent;

    public event EventHandler<PathChangeEventArgs> PathChangeEvent;

    public string FilePanelName { get; init; }

    private string _path;

    public string Path
    {
        get => _path;
        set
        {
            Set(ref _path, value);
            PathChangeEvent?.Invoke(this, new PathChangeEventArgs(value));
            Refresh();
        }
    }

    public ObservableCollection<IFilePanelItem> Files { get; set; } = new ObservableCollection<IFilePanelItem>();

    private IFilePanelItem _selectedFileItem;

    public IFilePanelItem SelectedFileItem
    {
        get => _selectedFileItem;
        set
        {
            Set(ref _selectedFileItem, value);
            // _selectedFileItem.IsSelected = !_selectedFileItem.IsSelected;
            FileSelectionChangeEvent?.Invoke(this, new FileSelectionChangeEventArgs(value));
            //_messageBus.Publish(new FileSelectionChangeEvent(_selectedFile, FilePanelName!));
            // OnPropertyChange("SelectedFile", ref value);
        }
    }

    private IFilePanelItem _focusedFileItem;

    public IFilePanelItem FocusedFileItem
    {
        get => _focusedFileItem;
        set
        {
            Set(ref _focusedFileItem, value);
        }
    }

    public void SelectItem(string sourceName)
    {
        MessageBox.Show(sourceName);
    }

    private ModelEventBus _messageBus;

    public FilePanelViewModel()
    {
        Path = @"c:\windows";
        SelectedFileItem = Files[0];
        _messageBus = ModelEventBus.Instance;
    }

    // public void OnPropertyChange<T>(string propertyName, ref T value)
    // {
    // }

    private void Refresh()
    {
        Files?.Clear();
        foreach (var item in GetFiles(Path))
            Files?.Add(item);
    }

    private ICollection<IFilePanelItem> GetFiles(string path)
    {
        var di = new DirectoryInfo(path);
        var directories = di.GetDirectories().Select(item => new DirectoryPanelItem(item) as IFilePanelItem);
        var files = di.GetFiles().Select(item => new FilePanelItem(item));
        var result = directories.Union(files);
        return result.ToArray();
    }
}
