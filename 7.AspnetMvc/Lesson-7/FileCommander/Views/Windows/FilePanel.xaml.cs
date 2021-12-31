using System;
using System.Collections.Generic;
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
using FileCommander.ViewModels;

namespace FileCommander.Views.Windows
{
    /// <summary>
    /// Interaction logic for FilePanel.xaml
    /// </summary>
    public partial class FilePanel : UserControl
    {
        public FilePanel()
        {
            InitializeComponent();
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is not ListViewItem listViewItem)
                return;

            if (ItemsControl.ItemsControlFromItemContainer(listViewItem) is not ListView listView)
                return;

            var context = (FilePanelViewModel)listView.DataContext;
            if (context.ChangeDirectoryCommand.CanExecute(null))
                context.ChangeDirectoryCommand.Execute(listViewItem.DataContext);
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is not ListView listView)
                return;

            if (listView.IsLoaded)
                CalculateListView(listView);
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is ListView listView)
                CalculateListView(listView);
        }

        private void CalculateListView(ListView listView)
        {
            if (listView.View is GridView gridView)
            {
                var actualWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
                for (int i = 1; i < gridView.Columns.Count; i++)
                {
                    actualWidth = actualWidth - gridView.Columns[i].ActualWidth;
                }
                gridView.Columns[0].Width = actualWidth;
            }
        }
    }
}
