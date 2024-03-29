using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FileCommander.Interop;
using FileCommander.ViewModels.Interfaces;

namespace FileCommander.ViewModels.Base;
public abstract class FilePanelItemBase : ViewModel, IFilePanelItem
{
    protected const string createdTemplate = "dd.MM.yy hh:mm";

    public ImageSource? Icon { get; init; }

    public abstract string Name { get; }

    public abstract string FullName { get; }

    public abstract string Created { get; }

    public virtual string Attributes { get; } = "";

    private bool _isSelected;

    public bool IsSelected
    {
        get => _isSelected;
        set => Set(ref _isSelected, value);
    }

    protected long _size;

    public string Size => FormatSize(_size);

    protected static ImageSource ToImageSource(string path)
    {
        var icon = FileIcons.GetIcon(path);
        if (icon is null)
            return null;

        ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
            icon.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());

        return imageSource;
    }

    protected static ImageSource ToImageSource(FileSystemInfo fileSystemInfo)
    {
        return ToImageSource(fileSystemInfo.FullName);
    }

    protected virtual string FormatSize(long size)
    {
        return "<DIR>";
    }
}
