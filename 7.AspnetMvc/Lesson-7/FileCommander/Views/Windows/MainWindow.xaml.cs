using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileCommander
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string[] items = { "test", "ewfewf" };
            string path = @"E:\Projects\";
            leftPathTextBox.KeyDown += PathTextBox_KeyDown;
            rightPathTextBox.KeyDown += PathTextBox_KeyDown;
            //rightPathTextBox.Text = path; 
            Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(@"C:\WINDOWS\system32\notepad.exe");
            //System.Drawing.Icon.FromHandle();

            //image.Save(@"e:\tmp\img1.bmp");

            rightListView.ItemsSource = GetFiles(path).Select(item =>
            {
                return new { Name = item.Name, Icon = ToImageSource(item) };
            });
        }

        public static ImageSource ToImageSource(string path)
        {

            var icon = System.Drawing.Icon.ExtractAssociatedIcon(path);

            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }

        public static ImageSource ToImageSource(FileSystemInfo fileSystemInfo)
        {
            if (!fileSystemInfo.Attributes.HasFlag(FileAttributes.Directory))
                return ToImageSource(fileSystemInfo.FullName);
            else
                return null;

        }

        private void PathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (e.Key == Key.Enter && Directory.Exists(textBox.Text))
            {
                Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(@"C:\WINDOWS\system32\notepad.exe");
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
            return di.GetFileSystemInfos();
        }
    }
}
