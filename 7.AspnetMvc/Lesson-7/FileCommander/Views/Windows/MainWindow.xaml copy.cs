// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Drawing;
// using System.IO;
// using System.Linq;
// using System.Runtime.InteropServices;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows;
// using System.Windows.Controls;
// using System.Windows.Data;
// using System.Windows.Documents;
// using System.Windows.Input;
// using System.Windows.Interop;
// using System.Windows.Media;
// using System.Windows.Media.Imaging;
// using System.Windows.Navigation;
// using System.Windows.Shapes;

// namespace FileCommander
// {
//     /// <summary>
//     /// Interaction logic for MainWindow.xaml
//     /// </summary>
//     public partial class MainWindow : Window
//     {
//         public MainWindow()
//         {
//             InitializeComponent();
//             string[] items = { "test", "ewfewf" };
//             string path = @"E:\Projects\";
//             leftPathTextBox.KeyDown += PathTextBox_KeyDown;
//             rightPathTextBox.KeyDown += PathTextBox_KeyDown;
//             //rightPathTextBox.Text = path; 
//             Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(@"C:\WINDOWS\system32\notepad.exe");
//             //System.Drawing.Icon.FromHandle();



//             As Raymond Chen blogged about some time ago, SHDefExtractIcon is your more powerful fallback if IExtractIcon::Extract(which is what the code sample above attempts to use) fails.The power of this function is its nIconSize parameter, which specifies the actual size of the icon that you want to extract.

// Adapting Raymond's example:

//     HICON ExtractArbitrarySizeIcon(LPCTSTR pszPath, int size)
//             {
//                 HICON hIcon;
//                 if (SHDefExtractIcon(pszPath, 1, 0, &hIcon, NULL, size) == S_OK)
//                 {
//                     return hIcon;
//                 }
//                 return NULL;  // failure
//             }
//             Whatever you do, remember that whenever an API function returns an HICON, it is transferring ownership of that resource to you.This means that, when you are finished with the icon, you must destroy it by calling the DestroyIcon function to avoid a leak.



//                        //unsafe
//                        //{
//                        //    int readIconCount = 0;
//                        //    IntPtr[] hDummy = new IntPtr[1] { IntPtr.Zero };
//                        //    IntPtr[] hIconEx = new IntPtr[1] { IntPtr.Zero };

//                        //    try
//                        //    {
//                        //        if (large)
//                        //            readIconCount = ExtractIconEx(file, 0, hIconEx, hDummy, 1);
//                        //        else
//                        //            readIconCount = ExtractIconEx(file, 0, hDummy, hIconEx, 1);

//                        //        if (readIconCount > 0 && hIconEx[0] != IntPtr.Zero)
//                        //        {
//                        //            // GET FIRST EXTRACTED ICON
//                        //            Icon extractedIcon = (Icon)Icon.FromHandle(hIconEx[0]).Clone();

//                        //            return extractedIcon;
//                        //        }
//                        //        else // NO ICONS READ
//                        //            return null;
//                        //    }
//                        //    catch (Exception ex)
//                        //    {
//                        //        /* EXTRACT ICON ERROR */

//                        //        // BUBBLE UP
//                        //        throw new ApplicationException("Could not extract icon", ex);
//                        //    }
//                        //    finally
//                        //    {
//                        //        // RELEASE RESOURCES
//                        //        foreach (IntPtr ptr in hIconEx)
//                        //            if (ptr != IntPtr.Zero)
//                        //                DestroyIcon(ptr);

//                        //        foreach (IntPtr ptr in hDummy)
//                        //            if (ptr != IntPtr.Zero)
//                        //                DestroyIcon(ptr);
//                        //    }
//                        //}




//                        //System.Drawing.Image image = ico.ToBitmap();

//                        //BitmapImage bi3 = new BitmapImage();
//                        //bi3.BeginInit();
//                        //bi3.UriSource = new Uri(@"e:\tmp\img1.png", UriKind.Absolute);
//                        //bi3.EndInit();

//                        Bitmap bitmap = ico.ToBitmap();
//             IntPtr hBitmap = bitmap.GetHbitmap();

//             ImageSource wpfBitmap =
//                  Imaging.CreateBitmapSourceFromHBitmap(
//                       hBitmap, IntPtr.Zero, Int32Rect.Empty,
//                       BitmapSizeOptions.FromEmptyOptions());

//             myImage.Source = ExtractAssociatedIconApi();



//             //image.Save(@"e:\tmp\img1.bmp");

//             rightListView.ItemsSource = GetFiles(path).Select(item =>
//             {
//                 return new { Name = item.Name, Icon = ToImageSource(item) };
//             });
//         }
//         #region  " Функции API "
//         [DllImport("shell32.dll")]
//         static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, StringBuilder lpIconPath, out ushort lpiIcon);
//         // ExtractIcon эта функция пока не задействована.
//         [DllImport("shell32.dll")]
//         static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);
//         #endregion

//         public static ImageSource ExtractAssociatedIconApi()
//         {
//             ushort uicon;
//             StringBuilder strB = new StringBuilder(@"C:\WINDOWS\system32\notepad.exe");
//             IntPtr handle = ExtractAssociatedIcon(Process.GetCurrentProcess().MainWindowHandle, strB, out uicon);

//             ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
//               handle,
//               Int32Rect.Empty,
//               BitmapSizeOptions.FromEmptyOptions());
//             return imageSource;
//             //Icon ico = Icon.FromHandle(handle);
//         }



//         public static ImageSource ToImageSource(string path)
//         {

//             var icon = System.Drawing.Icon.ExtractAssociatedIcon(path);

//             ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
//                 icon.Handle,
//                 Int32Rect.Empty,
//                 BitmapSizeOptions.FromEmptyOptions());

//             return imageSource;
//         }

//         public static ImageSource ToImageSource(FileSystemInfo fileSystemInfo)
//         {
//             if (!fileSystemInfo.Attributes.HasFlag(FileAttributes.Directory))
//                 return ToImageSource(fileSystemInfo.FullName);
//             else
//                 return null;

//         }

//         private void PathTextBox_KeyDown(object sender, KeyEventArgs e)
//         {
//             var textBox = sender as TextBox;
//             if (e.Key == Key.Enter && Directory.Exists(textBox.Text))
//             {
//                 Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(@"C:\WINDOWS\system32\notepad.exe");
//                 if (textBox.Name == "leftPathTextBox")
//                     leftListView.ItemsSource = GetFiles(textBox.Text).Select(item =>
//                     {
//                         return new { Name = item.Name, Icon = ToImageSource(item) };
//                     });
//                 else
//                     rightListView.ItemsSource = GetFiles(textBox.Text).Select(item =>
//                     {
//                         return new { Name = item.Name, Icon = ToImageSource(item) };
//                     });
//             }
//         }

//         private ICollection<FileSystemInfo> GetFiles(string path)
//         {
//             var di = new DirectoryInfo(path);
//             return di.GetFileSystemInfos();
//         }
//     }
// }
