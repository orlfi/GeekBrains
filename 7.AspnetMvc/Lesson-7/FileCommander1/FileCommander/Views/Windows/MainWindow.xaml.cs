using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
            // leftListView.ItemsSource = GetFileSystem(@"c:\windows");
            // .Select(item => new { Name = item.Name });
        }

        // private ICollection<FileSystemInfo> GetFileSystem(string path)
        // {
        //     var info = new DirectoryInfo(path);
        //     return info.GetFileSystemInfos();
        // }
    }
}
