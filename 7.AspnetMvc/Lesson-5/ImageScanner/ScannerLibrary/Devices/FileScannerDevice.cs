using System.Text;
using ScannerLibrary.Data;
using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Devices;


public class ScannerDevice : IScannerDevice
{
    // public event EventHandler<MonitorChangedEventArgs>? NewMonitorChanged;

    public event EventHandler<NewScanEventArgs>? NewScanEvent;

    private readonly FileSystemWatcher _watcher;
    private string _path;

    public ScannerDevice(string path)
    {
        _path = path;
        _watcher = new FileSystemWatcher(@"path");
        _watcher.Filter = "*.jpg";
        _watcher.IncludeSubdirectories = false;
    }



    public Stream Scan()
    {
        _watcher.NotifyFilter = NotifyFilters.Attributes
                             | NotifyFilters.CreationTime
                             | NotifyFilters.DirectoryName
                             | NotifyFilters.FileName
                             | NotifyFilters.LastAccess
                             | NotifyFilters.LastWrite
                             | NotifyFilters.Security
                             | NotifyFilters.Size;

        _watcher.Created += OnNewImage;
        _watcher.EnableRaisingEvents = true;

        Console.WriteLine("Press enter to exit.");
        Console.ReadLine();
        return File.OpenRead(_path);
    }

    private void OnNewImage(object sender, FileSystemEventArgs e)
    {
        if (!File.Exists(e.Name))
            return;
        var data = File.ReadAllBytes(e.Name);

        NewScanEvent?.Invoke(this, new NewScanEventArgs() { FileName = e.Name, Data = data });
    }
}
