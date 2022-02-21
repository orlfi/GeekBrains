using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Shapes;
using FileCommander.Commands.Base;
using FileCommander.ViewModels.Base;
using FileCommander.ViewModels.Interfaces;

namespace FileCommander.ViewModels;
public partial class FilePanelViewModel : ViewModel
{
    #region DeleteItemCommand
    private Command? _deleteItemCommand;

    public ICommand DeleteItemCommand => _deleteItemCommand ??= Command.Invoke(OnDeleteItemCommand).WithName("Удалить элемент");

    public void OnDeleteItemCommand(object? deletedItem)
    {
        if (deletedItem is IFilePanelItem item)
        {
            var index = Files.IndexOf(item);
            Files.RemoveAt(index);
            if (index < 0)
                index = 0;
            if (index >= Files.Count)
                index = Files.Count - 1;

            SelectedFileItem = Files.ElementAt(index);
        }
    }
    #endregion


    #region DoubleClickCommand
    private Command? _doubleClickCommand;

    public ICommand DoubleClickCommand => _doubleClickCommand ??= Command.Invoke(OnDoubleClickCommand);

    public void OnDoubleClickCommand(object? ev)
    {
        if (ev is null)
            return;

    }
    #endregion

    #region ChangeDirectoryCommand
    private Command? _changeDirectoryCommand;

    public ICommand ChangeDirectoryCommand => _changeDirectoryCommand ??= Command.Invoke(OnChangeDirectoryCommand).WithName("Удалить элемент");

    public void OnChangeDirectoryCommand(object? selectedItem)
    {
        if (selectedItem is null)
            return;

        Path = (selectedItem as IFilePanelItem).FullName;

        SelectedFileItem = Files.Count > 0 ? Files[0] : null;
    }
    #endregion

    #region ParentDirectoryCommand
    private Command? _parentDirectoryCommand;

    public ICommand ParentDirectoryCommand => _parentDirectoryCommand ??= Command.Invoke(OnParentDirectoryCommand).WithName("Удалить элемент");

    public void OnParentDirectoryCommand(object? parameter)
    {
        var parent = Directory.GetParent(_path);
        if (parent is not null)
        {
            Path = parent.FullName;
        }
    }
    #endregion

    #region SelectFileCommand
    private Command? _selectFileCommand;

    public ICommand SelectFileCommand => _selectFileCommand ??= Command.Invoke(OnSelectFileCommand);

    public void OnSelectFileCommand(object? parameter)
    {
        if (parameter is IFilePanelItem item)
        {
            item.IsSelected = !item.IsSelected;
        }

        // var parent = Directory.GetParent(_path);
        // if (parent is not null)
        // {
        //     Path = parent.FullName;
        // }
    }
    #endregion

    #region ReportFileCommand
    private Command? _reportCommand;

    public ICommand ReportCommand => _reportCommand ??= Command.Invoke(OnReportCommand);

    public void OnReportCommand(object? parameter)
    {
        var fileItem = parameter as IFilePanelItem;
        if (fileItem is not null)
            CreateReportEvent?.Invoke(this, new ModelEvents.CreateReportEventArgs(fileItem.FullName));
    }
    #endregion
}
