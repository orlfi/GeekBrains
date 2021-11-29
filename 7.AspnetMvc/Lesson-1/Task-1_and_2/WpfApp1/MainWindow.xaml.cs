using System;
using System.Threading;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int number = 1;
        private NumberFormatInfo f = new NumberFormatInfo { NumberGroupSeparator = " " };
        private volatile bool executing = false;

        public MainWindow()
        {
            InitializeComponent();
            textBox.IsReadOnly = true;
            SetTime();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetTime();
        }

        private void SetTime()
        {
            if (textBox != null)
            {
                string str = Math.Round(slider.Value, 1, MidpointRounding.AwayFromZero).ToString("0.0", f);
                textBox.Text = str;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            executing = !executing;
            if (executing)
            {
                ((Button)sender).Content = "Стоп";
                Task task = Task.Run(() => AddNumberAsync());
                task.Start();   
            }
            else
            {
                ((Button)sender).Content = "Старт";
            }
        }

        private void AddNumberAsync() 
        {
            while (true)
            {
                if (!executing)
                    break;
                number++;
                listBox.Dispatcher.Invoke(() =>
                {
                    listBox.Items.Add(FibFor(number).ToString("#,###", f));
                    listBox.Items.MoveCurrentToLast();
                    listBox.ScrollIntoView(listBox.Items.CurrentItem);
                });
                double sleep = slider.Dispatcher.Invoke<double>(() => slider.Value) * 1000;
                Thread.Sleep((int)Math.Round(sleep, 0, MidpointRounding.AwayFromZero));
            }
        }

        // Слишком медленно!!!
        private ulong FibRecursive(ulong n)
        {
            if (n == 0 || n == 1) return n;
            return FibRecursive(n - 1) + FibRecursive(n - 2);
        }

        private ulong FibFor(int n)
        {
            ulong result = 0;
            ulong b = 1;
            ulong tmp;

            for (int i = 0; i < n; i++)
            {
                tmp = result;
                result = b;
                b += tmp;
            }

            return result;
        }
    }
}
