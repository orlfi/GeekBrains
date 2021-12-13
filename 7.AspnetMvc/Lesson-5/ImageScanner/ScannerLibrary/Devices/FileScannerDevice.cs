using System.IO;
using System.Text;
using ScannerLibrary.Data;
using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Devices;

public class FileScannerDevice : IScannerDevice
{
    public event EventHandler<NewScanEventArgs>? NewScanEvent;

    private readonly FileSystemWatcher _watcher;

    private string _path;

    public FileScannerDevice(string path)
    {
        _path = path;
        _watcher = new FileSystemWatcher(path);
        _watcher.Filter = "*.bmp";
        _watcher.IncludeSubdirectories = false;
    }

    public void Scan()
    {
        _watcher.Created += OnNewImage;
        _watcher.EnableRaisingEvents = true;
    }

    private void OnNewImage(object sender, FileSystemEventArgs e)
    {
        if (!File.Exists(e.Name))
            return;

        var picture = new Picture() { Data = File.ReadAllBytes(e.Name) };

        NewScanEvent?.Invoke(this, new NewScanEventArgs() { FileName = Path.GetFileName(e.Name), Picture = picture });
    }
}
