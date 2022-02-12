using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FileCommander.Interop;

namespace FileCommander;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // string[] items = { "test", "ewfewf" };
        // string path = @"c:\windows\";
        // leftPathTextBox.KeyDown += PathTextBox_KeyDown;
        // rightPathTextBox.KeyDown += PathTextBox_KeyDown;

        // var files = GetFiles(path).Select(item =>
        // {
        //     return new { Name = item.Name, Icon = ToImageSource(item) };
        // }).ToList();

        // leftListView.ItemsSource = files;
        // rightListView.ItemsSource = files;
    }

    public static ImageSource ToImageSource(string path)
    {

        var icon = FileIcons.GetIcon(path);

        ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
            icon.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());

        return imageSource;
    }

    public static ImageSource ToImageSource(FileSystemInfo fileSystemInfo)
    {
        return ToImageSource(fileSystemInfo.FullName);
    }

    private void PathTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        var textBox = sender as TextBox;
        if (e.Key == Key.Enter && Directory.Exists(textBox.Text))
        {
            if (textBox.Name == "leftPathTextBox")
                leftListView.ItemsSource = GetFiles(textBox.Text).Select(item =>
                {
                    return new { Name = item.Name, Icon = ToImageSource(item) };
                });
            else
                rightListView.ItemsSource = GetFiles(textBox.Text).Select(item =>
                {
                    return new { Name = item.Name, Icon = ToImageSource(item) };
                });
        }
    }

    private ICollection<FileSystemInfo> GetFiles(string path)
    {
        var di = new DirectoryInfo(path);
        var directories = di.GetFileSystemInfos().Where(item => item.Attributes.HasFlag(FileAttributes.Directory));
        var files = di.GetFileSystemInfos().Where(item => !item.Attributes.HasFlag(FileAttributes.Directory));
        var result = directories.Union(files);
        return result.ToArray();
    }
}

